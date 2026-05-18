using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Cài đặt Scene")]
    [Tooltip("Nhập tên Scene Trang chủ")]
    [SerializeField] private string homeSceneName;
    [Space(10)]
    [Header("Cài đặt ngôn ngữ")]
    public TextMeshProUGUI textLanguage;
    public string vietnameseText = "Ngôn ngữ";
    public string englishText = "Language";

    [Space(10)]
    [Header("Cảnh báo tienegs")]
    public List<TextMeshProUGUI> textElementsWarning = new List<TextMeshProUGUI>();
    public List<string> vietnameseWarnings = new List<string>();
    public List<string> englishWarnings = new List<string>();

    void Start()
    {
        UpdateLanguageText();
    }

    private void UpdateLanguageText()
    {
        if (LanguageManager.Instance != null)
        {
            textLanguage.text = LanguageManager.Instance.currentLanguage == 0 ? vietnameseText : englishText;
            for (int i = 0; i < textElementsWarning.Count; i++)
            {
                if (i < vietnameseWarnings.Count && i < englishWarnings.Count)
                {
                    textElementsWarning[i].text = LanguageManager.Instance.currentLanguage == 0 ? vietnameseWarnings[i] : englishWarnings[i];
                }
            }
        }
    }
    

    public void GoToHome()
    {
        if (!string.IsNullOrEmpty(homeSceneName))
        {
        
            SceneManager.LoadScene(homeSceneName);
        }
        else
        {
            Debug.LogWarning("Bạn chưa nhập tên Home Scene trong Inspector!");
        }
    }


    public void RestartScene()
    {
  
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


    public void QuitApp()
    {
        Debug.Log("Đã thoát ứng dụng!");
        
        
        Application.Quit();

      
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

/// <summary>
/// // thêm tạm vào đây cho nhanh, sau này có thể tách ra ngôn ngữ riêng
/// </summary>

    public void Btn_SelectVietnamese()
    {
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.SetVietnameseViet();
           
        }
    }

    public void Btn_SelectEnglish()
    {
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.SetEnglishViet();
         
        }
    }

    public void Btn_Confirm()
    {
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.ConfirmLanguage();
        }
    }
}