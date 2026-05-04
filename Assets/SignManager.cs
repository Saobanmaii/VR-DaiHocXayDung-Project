using UnityEngine;
using UnityEngine.UI; 

public class SignManager : MonoBehaviour
{
    [Header("Kéo Object chứa SignDragHandler vào đây")]
    public SignDragHandler dragHandler; 
    
    [Header("Kéo file Data biển báo tương ứng vào đây")]
    public WarningSpriteData myData;

    // Hàm này KHÔNG CÓ THAM SỐ, nên Unity Inspector sẽ nhìn thấy!
    public void TriggerClick()
    {
        if (dragHandler != null)
        {
            // Tự động truyền data và chính bản thân cái nút này (this.gameObject) vào hàm gốc
            dragHandler.clickButton(myData, this.gameObject);
        }
    }
}