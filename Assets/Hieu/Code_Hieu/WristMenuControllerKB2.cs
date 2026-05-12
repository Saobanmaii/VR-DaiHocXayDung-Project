using UnityEngine;
using UnityEngine.InputSystem;

public class WristMenuControllerKB2 : MonoBehaviour
{
    public InputActionProperty gripAction; // Kéo Action Grip của tay trái vào đây
    public GameObject menuCanvas; // Kéo cái bảng Menu vào đây

    void Update()
    {
        // Đọc giá trị nút Grip (thường từ 0 đến 1)
        float gripValue = gripAction.action.ReadValue<float>();

        // Nếu bóp tay (Grip > 0.5) thì hiện, thả ra thì ẩn
        if (gripValue > 0.5f)
        {
            menuCanvas.SetActive(true);
        }
        else
        {
            menuCanvas.SetActive(false);
        }
    }
}