using System.Collections; // Bắt buộc phải có dòng này để dùng thời gian chờ
using UnityEngine;

public class warningDelay : MonoBehaviour
{
    [Header("Kéo thả UI và Âm thanh vào đây")]
    public GameObject uiCanhBao;
    public AudioSource coiBaoDong;

    // Biến này để lưu lại bộ đếm thời gian, phòng khi muốn huỷ nó
    private Coroutine boDemThoiGian;

    void Start()
    {
        // Ẩn UI lúc mới bắt đầu game
        if (uiCanhBao != null)
        {
            uiCanhBao.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. Nếu đang đếm ngược 10 giây để tắt mà người chơi lại bước vào, thì HUỶ đếm ngược
        if (boDemThoiGian != null)
        {
            StopCoroutine(boDemThoiGian);
        }

        // 2. Hiện UI
        if (uiCanhBao != null) uiCanhBao.SetActive(true);

        // 3. Bật còi (nếu chưa kêu)
        if (coiBaoDong != null && !coiBaoDong.isPlaying) coiBaoDong.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        // Khi người chơi đi ra khỏi trigger, bắt đầu chạy bộ đếm 10 giây
        boDemThoiGian = StartCoroutine(Doi10GiayRoiTat());
    }

    // Hàm đếm thời gian
    IEnumerator Doi10GiayRoiTat()
    {
        // Lệnh bắt Unity ngồi đợi đúng 5 giây
        yield return new WaitForSeconds(5f);

        // Sau khi đợi xong 10 giây thì mới chạy lệnh tắt bên dưới
        if (uiCanhBao != null) uiCanhBao.SetActive(false);
        if (coiBaoDong != null) coiBaoDong.Stop();
    }
}