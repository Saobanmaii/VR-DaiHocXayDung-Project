using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Cài đặt Scene")]
    [Tooltip("Nhập tên Scene Trang chủ của bạn vào đây (ví dụ: MainMenuScene)")]
    [SerializeField] private string homeSceneName;


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

        // Dừng chế độ Play khi đang chạy test trong Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}