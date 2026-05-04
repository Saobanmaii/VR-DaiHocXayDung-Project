using UnityEngine;
using UnityEngine.InputSystem; 

public class SignDragHandler : MonoBehaviour
{
    public UIControllerSprireWarning uiController;
    private UIControllerSprireWarning draggedItem;

    [Header("Cài đặt VR")]
    public Transform vrPointerOrigin; 
    
    // Kéo action "Select" hoặc "Trigger" của Controller vào đây
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
       
        if(leftTriggerAction.action.IsPressed())
        {
            canvasWarning.SetActive(true);
        }
        else
        {
            canvasWarning.SetActive(false);
        }

        
        if (draggedItem != null)
        {
            bool isTriggering = triggerAction.action.IsPressed();

            if (isTriggering) 
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
            else 
            {
                Collider col = draggedItem.GetComponent<Collider>();
                if (col != null) col.enabled = true;
                
              
                draggedItem.transform.localScale = originalScale;
                
                draggedItem = null;
            }
        }
    }
}