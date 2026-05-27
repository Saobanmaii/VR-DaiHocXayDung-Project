using UnityEngine;
using System.Runtime.InteropServices;

public class QuizManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SubmitResult(float score, string dataJson);

    // Bảng điểm THỰC TẾ
    [Header("Cài đặt Game")]
    public int tongSoCau = 5; // A tự sửa số này trên Inspector cho đúng số câu hỏi của a

    [Header("Bảng điểm (Không cần sửa)")]
    public int soCauDung = 0; // Ban đầu chưa đúng câu nào
    private float thoiGianBatDau;

    void Start()
    {
        // Ghi lại mốc thời gian lúc bắt đầu chạy game
        thoiGianBatDau = Time.time;
    }

    // 👉 Hàm này sẽ được gọi khi bấm vào đáp án ĐÚNG
    public void ChonDapAnDung()
    {
        soCauDung++; // Cộng 1 vào số câu đúng
        Debug.Log("✔️ Bấm chuẩn! Tổng câu đúng hiện tại: " + soCauDung);

        // (A có thể thêm code tắt bảng câu hỏi hiện tại và bật bảng tiếp theo ở đây)
    }

    // 👉 Hàm này sẽ được gọi khi bấm vào đáp án SAI
    public void ChonDapAnSai()
    {
        Debug.Log("❌ Sai rồi, không được cộng điểm!");

        // (A có thể thêm code tắt bảng câu hỏi hiện tại và bật bảng tiếp theo ở đây)
    }

    // 👉 Hàm này được gọi ở câu cuối cùng hoặc khi bấm nút "Nộp bài"
    public void KetThucVaGuiDiem()
    {
        // Tự động tính số giây đã trôi qua từ lúc mở game
        float thoiGianLamBai = Time.time - thoiGianBatDau;

        // Tính điểm hệ số 10
        float diemSo = ((float)soCauDung / tongSoCau) * 10f;
        diemSo = Mathf.Clamp(diemSo, 0f, 10f);

        // Gói dữ liệu
        string duLieuJson = "{\"correct\":" + soCauDung + ",\"total\":" + tongSoCau + ",\"time_seconds\":" + Mathf.RoundToInt(thoiGianLamBai) + "}";

        // Gửi ra Web
#if UNITY_WEBGL == true && UNITY_EDITOR == false
            SubmitResult(diemSo, duLieuJson);
#else
        Debug.Log($"[Giả lập WebGL] Đã gửi điểm THẬT: {diemSo} | Dữ liệu: {duLieuJson}");
#endif
    }
}