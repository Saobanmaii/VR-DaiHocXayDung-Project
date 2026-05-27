using UnityEngine;
// Bắt buộc phải thêm dòng này để gọi thư viện của hệ thống nhận nút mới
using UnityEngine.InputSystem;

public class MouseClick : MonoBehaviour
{
    // Biến để lưu trữ tham chiếu tới camera được gắn script này
    private Camera myCamera;

    void Start()
    {
        // Lấy component Camera trên cùng object
        myCamera = GetComponent<Camera>();

        if (myCamera == null)
        {
            Debug.LogError("Script MouseClick phải được gắn vào một GameObject có Camera!");
        }
    }

    void Update()
    {
        // Nếu không tìm thấy camera, không làm gì cả
        if (myCamera == null) return;

        // KIỂM TRA CLICK CHUỘT THEO HỆ THỐNG MỚI:
        // Nếu có chuột và nút chuột trái vừa được bấm xuống trong frame này
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("🔴 Đã nhận tín hiệu click chuột!"); // Bỏ comment dòng này để test nếu cần

            // Lấy tọa độ chuột trên màn hình
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // Tạo tia ray bắn từ vị trí camera
            Ray ray = myCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            // Bắn tia ray và kiểm tra va chạm
            if (Physics.Raycast(ray, out hit))
            {
                // Kiểm tra xem vật bị bắn trúng có script ErrorItem không
                ErrorItem clickedError = hit.collider.GetComponent<ErrorItem>();

                // Nếu đúng là cục lỗi, thì gọi hàm click y hệt như lúc a bóp cò VR
                if (clickedError != null)
                {
                    clickedError.OnErrorClicked();
                }
            }
        }
    }
}