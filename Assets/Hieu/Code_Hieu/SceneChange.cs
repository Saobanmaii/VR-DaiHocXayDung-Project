using UnityEngine;
using UnityEngine.SceneManagement; // Bắt buộc phải khai báo thư viện này ở trên cùng

public class SceneChange : MonoBehaviour
{
    // Hàm này nhận vào tên của Scene mà a muốn chuyển tới
    public void LoadMyScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // (Tùy chọn) Thêm hàm này nếu a muốn làm nút Thoát Game
    public void QuitGame()
    {
        Debug.Log("Đã thoát game!");
        Application.Quit();
    }
}