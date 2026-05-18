using UnityEngine;
using UnityEngine.UI; // Cần thư viện này để thao tác với hình ảnh nút

public class LanguageButton : MonoBehaviour
{
    [Header("Liên kết Nút (Kéo thả 2 nút vào đây)")]
    public Button vietnameseButton;
    public Button englishButton;

    [Header("Cài đặt Màu sắc")]
    public Color activeColor = Color.white; // Màu khi nút được CHỌN (VD: Trắng đậm)
    public Color normalColor = new Color(0.7f, 0.7f, 0.7f, 1f); // Màu khi nút KHÔNG được chọn (VD: Xám mờ)

    void Start()
    {
        // Khi scene vừa load, phải ngó xem Manager đang chọn tiếng gì để tô màu nút cho đúng
        Invoke("UpdateVisuals", 0.1f); // Chờ 0.1 giây để đảm bảo Manager đã Singleton xong
    }

    // Hàm gắn vào nút chọn Tiếng Việt
    public void ClickVietnamese()
    {
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.SetVietnamese();
            UpdateVisuals(); // Click xong thì cập nhật màu
        }
    }

    // Hàm gắn vào nút chọn Tiếng Anh
    public void ClickEnglish()
    {
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.SetEnglish();
            UpdateVisuals(); // Click xong thì cập nhật màu
        }
    }

    // Hàm cốt lõi để xử lý việc đổi màu dựa trên ngôn ngữ đang chọn
    private void UpdateVisuals()
    {
        if (LanguageManager.Instance == null || vietnameseButton == null || englishButton == null) return;

        int currentLang = LanguageManager.Instance.currentLanguage;

        // Lấy thành phần Image của nút để đổi màu (color tint)
        Image vnImg = vietnameseButton.GetComponent<Image>();
        Image enImg = englishButton.GetComponent<Image>();

        if (vnImg == null || enImg == null) return;

        // Nếu đang là Tiếng Việt (0)
        if (currentLang == 0)
        {
            vnImg.color = activeColor; // Nút Việt sáng lên
            enImg.color = normalColor; // Nút Anh mờ đi
        }
        // Nếu đang là Tiếng Anh (1)
        else if (currentLang == 1)
        {
            vnImg.color = normalColor; // Nút Việt mờ đi
            enImg.color = activeColor; // Nút Anh sáng lên
        }
    }
}