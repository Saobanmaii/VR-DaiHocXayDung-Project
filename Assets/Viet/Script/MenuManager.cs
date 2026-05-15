using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Cài đặt Scene")]
    [Tooltip("Nhập tên Scene Trang chủ")]
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

      
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}