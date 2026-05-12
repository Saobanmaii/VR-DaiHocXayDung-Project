using UnityEngine;

public class HienThiUIDonGian : MonoBehaviour
{
    [Header("Kéo thả UI và Âm thanh vào đây")]
    public GameObject uiCanhBao;   // Kéo cụm UI vào đây
    public AudioSource coiBaoDong; // Kéo vật chứa còi vào đây

    void Start()
    {
        // Ẩn UI lúc mới bắt đầu game
        if (uiCanhBao != null)
        {
            uiCanhBao.SetActive(false);
        }
    }

    // BẤT CỨ VẬT GÌ chạm vào Trigger đều chạy hàm này
    private void OnTriggerEnter(Collider other)
    {
        // Hiện UI
        if (uiCanhBao != null) uiCanhBao.SetActive(true);

        // Bật còi (nếu chưa kêu)
        if (coiBaoDong != null && !coiBaoDong.isPlaying) coiBaoDong.Play();
    }

    // BẤT CỨ VẬT GÌ đi ra khỏi Trigger đều chạy hàm này
    private void OnTriggerExit(Collider other)
    {
        // Ẩn UI
        if (uiCanhBao != null) uiCanhBao.SetActive(false);

        // Tắt còi
        if (coiBaoDong != null) coiBaoDong.Stop();
    }
}