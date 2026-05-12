using UnityEngine;
using TMPro; // Sử dụng TextMeshPro cho UI

public class ErrorManager : MonoBehaviour
{
    [Header("Cài đặt Lỗi")]
    public int totalErrors; // Tổng số lỗi cần tìm
    private int foundErrors = 0; // Số lỗi đã tìm thấy

    [Header("UI & Sự kiện")]
    public TextMeshProUGUI errorText; // Text hiển thị (VD: "Lỗi: 0/5")
    public GameObject completePanel;  // Object/UI sẽ hiện lên khi tìm đủ
    public GameObject uiToHideOnComplete; // THÊM MỚI: Object/UI sẽ bị ẩn đi khi tìm đủ

    void Start()
    {
        UpdateUI();

        // Đảm bảo panel hoàn thành bị ẩn lúc ban đầu
        if (completePanel != null)
        {
            completePanel.SetActive(false);
        }

        // THÊM MỚI: Đảm bảo UI cần ẩn đang được bật lúc bắt đầu
        if (uiToHideOnComplete != null)
        {
            uiToHideOnComplete.SetActive(true);
        }
    }

    // Hàm này sẽ được gọi khi người dùng click trúng một lỗi
    public void ReportErrorFound()
    {
        foundErrors++;
        UpdateUI();

        // Kiểm tra điều kiện: Nếu tìm đủ số lỗi
        if (foundErrors >= totalErrors)
        {
            TriggerCompleteEvent();
        }
    }

    private void UpdateUI()
    {
        if (errorText != null)
        {
            errorText.text = $"{foundErrors} / {totalErrors}";
        }
    }

    private void TriggerCompleteEvent()
    {
        // Hiện panel hoàn thành
        if (completePanel != null)
        {
            completePanel.SetActive(true);
        }

        // THÊM MỚI: Ẩn UI mong muốn khi đã tìm đủ
        if (uiToHideOnComplete != null)
        {
            uiToHideOnComplete.SetActive(false);
        }

        Debug.Log("Đã tìm đủ tất cả các lỗi trong mô hình BIM!");
    }
}