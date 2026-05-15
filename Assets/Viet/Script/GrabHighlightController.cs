using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class GrabHighlightController : MonoBehaviour
{
    [Tooltip("Kéo object chứa GlobalSocketHighlighter vào đây")]
    public GlobalSocketHighlighter highlighter; 
    
    private XRGrabInteractable _grabInteractable;

    private void Awake()
    {
        
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
    
        _grabInteractable.selectEntered.AddListener(OnGrab);
        _grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        
        _grabInteractable.selectEntered.RemoveListener(OnGrab);
        _grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {

        if (!(args.interactorObject is XRSocketInteractor))
        {
            if (highlighter != null) 
            {
                highlighter.HighlightSockets();
            }
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {

        if (!(args.interactorObject is XRSocketInteractor))
        {
            if (highlighter != null) 
            {
                highlighter.UnHighlightSockets();
            }
        }
    }
}