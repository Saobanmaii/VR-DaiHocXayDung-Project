using UnityEngine;
using TMPro; // Sử dụng TextMeshPro

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    [Header("Nội dung dịch")]
    [TextArea(2, 5)] public string vnText; // Chữ Tiếng Việt
    [TextArea(2, 5)] public string enText; // Chữ Tiếng Anh

    private TextMeshProUGUI textUI;

    void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();

        // Đăng ký lắng nghe sự kiện đổi ngôn ngữ
        LanguageManager.OnLanguageChanged += UpdateText;

        // Cập nhật chữ ngay khi vừa vào scene
        UpdateText();
    }

    void OnDestroy()
    {
        // Hủy đăng ký khi đổi scene để tránh lỗi bộ nhớ
        LanguageManager.OnLanguageChanged -= UpdateText;
    }

    public void UpdateText()
    {
        if (textUI == null || LanguageManager.Instance == null) return;

        // Nếu số là 0 thì hiện chữ Việt, là 1 thì hiện chữ Anh
        if (LanguageManager.Instance.currentLanguage == 0) textUI.text = vnText;
        else if (LanguageManager.Instance.currentLanguage == 1) textUI.text = enText;
    }
}