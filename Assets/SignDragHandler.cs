using UnityEngine;
using UnityEngine.InputSystem; 

public class SignDragHandler : MonoBehaviour
{
    public UIControllerSprireWarning uiController;
    private UIControllerSprireWarning draggedItem;

    [Header("Cài đặt VR")]
    public Transform vrPointerOrigin; 
    public InputActionReference triggerAction; 
    public InputActionReference leftTriggerAction; 
    [SerializeField] GameObject canvasWarning;
    
    [Header("Cài đặt thả biển báo")]
    public float yOffset = 0.5f;
    public LayerMask placementLayer = ~0;

    private Vector3 originalScale;

    void Start()
    {
       canvasWarning.SetActive(false); 
    }
    
    public void clickButton(WarningSpriteData data, GameObject _gameobject)
    {
        draggedItem = Instantiate(uiController, _gameobject.transform.position, Quaternion.identity);
        draggedItem.SetUp(data);

        originalScale = draggedItem.transform.localScale;

        Collider col = draggedItem.GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }

    void Update()
    {
        // Bật tắt Canvas bằng cò trái (giữ nguyên của bạn)
        if(leftTriggerAction.action.IsPressed())
        {
            canvasWarning.SetActive(true);
        }
        else
        {
            canvasWarning.SetActive(false);
        }

        // --- SỬA LỖI DRAG Ở ĐÂY ---
        if (draggedItem != null)
        {
            // Đọc trực tiếp lực bóp cò (trả về float từ 0.0 đến 1.0)
            float triggerValue = triggerAction.action.ReadValue<float>();

            // Nếu lực bóp lớn hơn 0.1 (tức là đang nhấn giữ, tránh việc cò bị lỏng lò xo)
            if (triggerValue > 0.1f) 
            {
                Ray ray = new Ray(vrPointerOrigin.position, vrPointerOrigin.forward);
                
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, placementLayer))
                {
                    draggedItem.transform.position = hit.point + (hit.normal * yOffset);
                    draggedItem.transform.up = hit.normal;
                    draggedItem.transform.localScale = originalScale;
                }
                else
                {
                    draggedItem.transform.position = ray.origin + (ray.direction * 0.2f);
                    draggedItem.transform.localScale = originalScale * 0.1f;
                }
            }
            else // Chỉ thả ra khi thực sự nhả cò (triggerValue <= 0.1)
            {
                Collider col = draggedItem.GetComponent<Collider>();
                if (col != null) col.enabled = true;
                
                draggedItem.transform.localScale = originalScale;
                
                // Giải phóng object, kết thúc việc kéo thả
                draggedItem = null;
            }
        }
    }
}