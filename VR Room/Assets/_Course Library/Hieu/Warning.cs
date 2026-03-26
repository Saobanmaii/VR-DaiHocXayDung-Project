using UnityEngine;

public class HienThiUIDonGian : MonoBehaviour
{
    [Header("Kéo thả UI, Âm thanh và Rào chắn vào đây")]
    public GameObject uiCanhBao;   // Kéo cụm UI vào đây
    public AudioSource coiBaoDong; // Kéo vật chứa còi vào đây
    public GameObject raoChan;     // Kéo vật thể Rào chắn (bức tường lưới mờ) vào đây

    void Start()
    {
        // Ẩn UI lúc mới bắt đầu game
        if (uiCanhBao != null)
        {
            uiCanhBao.SetActive(false);
        }

        // Ẩn Rào chắn lúc mới bắt đầu game
        if (raoChan != null)
        {
            raoChan.SetActive(false);
        }
    }

    // BẤT CỨ VẬT GÌ chạm vào Trigger đều chạy hàm này
    private void OnTriggerEnter(Collider other)
    {
        // Hiện UI
        if (uiCanhBao != null) uiCanhBao.SetActive(true);

        // Bật còi (nếu chưa kêu)
        if (coiBaoDong != null && !coiBaoDong.isPlaying) coiBaoDong.Play();

        // Hiện Rào chắn lên
        if (raoChan != null) raoChan.SetActive(true);
    }

    // BẤT CỨ VẬT GÌ đi ra khỏi Trigger đều chạy hàm này
    private void OnTriggerExit(Collider other)
    {
        // Ẩn UI
        if (uiCanhBao != null) uiCanhBao.SetActive(false);

        // Tắt còi
        if (coiBaoDong != null) coiBaoDong.Stop();

        // CỐ TÌNH KHÔNG LÀM GÌ VỚI RÀO CHẮN Ở ĐÂY ĐỂ NÓ HIỆN VĨNH VIỄN
        if (raoChan != null) raoChan.SetActive(false);
    }
}