using UnityEngine;

public class TriggerShowUI : MonoBehaviour
{
    [Header("Giao diện (UI)")]
    public GameObject uiToShow; // Kéo UI cần hiện vào đây

    [Header("Cài đặt")]
    public string targetTag = "Player"; // Tag của người chơi
    public bool hideOnExit = true;      // Tích vào nếu muốn rời đi thì ẩn UI

    void Start()
    {
        // Đảm bảo UI được ẩn đi khi mới vào game
        if (uiToShow != null)
        {
            uiToShow.SetActive(false);
        }
    }

    // Hàm này tự động chạy khi có vật thể chạm vào Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem vật chạm vào có đúng là người chơi (mang tag Player) không
        if (other.CompareTag(targetTag))
        {
            if (uiToShow != null)
            {
                uiToShow.SetActive(true);
            }
        }
    }

    // Hàm này tự động chạy khi vật thể đi ra khỏi Trigger
    private void OnTriggerExit(Collider other)
    {
        // Nếu bật chế độ ẩn khi rời đi và đúng là người chơi vừa đi ra
        if (hideOnExit && other.CompareTag(targetTag))
        {
            if (uiToShow != null)
            {
                uiToShow.SetActive(false);
            }
        }
    }
}