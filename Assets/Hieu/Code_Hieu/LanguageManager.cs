using UnityEngine;
using System;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    public static Action OnLanguageChanged;

    [Header("Cài đặt (0 = VN, 1 = EN)")]
    public int currentLanguage = 0;

    void Awake()
    {
        // Singleton giúp giữ cục quản lý này sống xuyên scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Tự động load ngôn ngữ đã lưu từ trước (mặc định là 0 - Tiếng Việt)
        currentLanguage = PlayerPrefs.GetInt("SavedLanguage", 0);
    }

    // Hàm gắn vào nút chọn Tiếng Việt
    public void SetVietnamese()
    {
        currentLanguage = 0;
        SaveAndApply();
    }

    // Hàm gắn vào nút chọn Tiếng Anh
    public void SetEnglish()
    {
        currentLanguage = 1;
        SaveAndApply();
    }

    private void SaveAndApply()
    {
        PlayerPrefs.SetInt("SavedLanguage", currentLanguage);
        PlayerPrefs.Save();

        // Phát loa thông báo cho mọi chữ ở scene hiện tại tự đổi theo
        if (OnLanguageChanged != null)
        {
            OnLanguageChanged();
        }
    }
}