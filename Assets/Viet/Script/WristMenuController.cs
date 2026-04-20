using UnityEngine;

public class WristMenuController : MonoBehaviour
{
    [Header("Thành phần chính")]
    public Transform leftHand;       // Gắn Controller tay trái vào đây
    public GameObject canvasUI;      // Gắn Canvas hiển thị 2/8 lỗi vào đây

    [Header("Cài đặt góc xoay (Trục Z)")]
    [Tooltip("Góc nhỏ nhất để bắt đầu hiện UI")]
    public float minZAngle = 54f;    
    
    [Tooltip("Góc lớn nhất UI còn hiển thị")]
    public float maxZAngle = 130f;   

    
    private bool isMenuActive = false;

    void Start()
    {
        
        if (canvasUI != null) 
        {
            canvasUI.SetActive(false);
        }
    }

    void Update()
    {
        if (leftHand == null || canvasUI == null) return;

        CheckWristZRotation();
    }

    private void CheckWristZRotation()
    {
       
        float currentZAngle = leftHand.localEulerAngles.z;

      
        bool shouldShowMenu = (currentZAngle >= minZAngle && currentZAngle <= maxZAngle);

        
        if (shouldShowMenu != isMenuActive)
        {
            isMenuActive = shouldShowMenu;
            canvasUI.SetActive(isMenuActive);
        }
    }
}