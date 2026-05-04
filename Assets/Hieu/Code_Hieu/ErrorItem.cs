using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ErrorItem : MonoBehaviour
{
    [Header("Liên kết (References)")]
    public ErrorManager errorManager;
    public Collider errorCollider;    // Kéo Collider của cục lỗi vào đây
    public GameObject checkmarkUI;    // Kéo cục UI chứa dấu tích xanh vào đây

    private bool isFound = false;

    void Start()
    {
        // Tự động lấy Collider trên object này nếu a quên kéo thả
        if (errorCollider == null)
            errorCollider = GetComponent<Collider>();

        // Đảm bảo dấu tích xanh bị ẩn khi mới bắt đầu
        if (checkmarkUI != null)
            checkmarkUI.SetActive(false);
    }

    // Hàm này sẽ được gọi khi a dùng Ray trỏ vào và bấm nút Click
    public void OnErrorClicked()
    {
        if (isFound) return; // Nếu đã tìm thấy rồi thì thoát, không chạy đoạn dưới nữa

        isFound = true;

        // 1. Tắt Collider để tia ray không còn nhận diện cục này nữa (nhưng mesh vẫn còn nếu a muốn)
        if (errorCollider != null)
        {
            errorCollider.enabled = false;
        }

        // 2. Hiện dấu tích xanh lên
        if (checkmarkUI != null)
        {
            checkmarkUI.SetActive(true);
        }

        // 3. Báo về cho Manager để cộng UI số lượng tổng
        if (errorManager != null)
        {
            errorManager.ReportErrorFound();
        }
    }
}