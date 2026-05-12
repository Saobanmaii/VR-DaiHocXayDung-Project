using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRRayInteractor))]
[RequireComponent(typeof(XRInteractorLineVisual))]
public class ShortenRayOnGrab : MonoBehaviour
{
    [Header("Cài đặt tia Ray")]
    [Tooltip("Độ dài của tia Ray SAU KHI tóm vật thể (tính bằng mét)")]
    public float shortLength = 2f; 

    private XRRayInteractor rayInteractor;
    private XRInteractorLineVisual lineVisual;
    
    // Lưu lại thông số ban đầu để trả về khi thả tay ra
    private float originalLength;
    private bool originalStopAtSelection;

    void Awake()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        lineVisual = GetComponent<XRInteractorLineVisual>();
        
        // Lưu lại độ dài gốc (thường là 10m hoặc 30m)
        if (lineVisual != null)
        {
            originalLength = lineVisual.lineLength;
            originalStopAtSelection = lineVisual.stopLineAtSelection;
        }
    }

    void OnEnable()
    {
        // Lắng nghe sự kiện tóm và thả vật thể
        rayInteractor.selectEntered.AddListener(OnGrab);
        rayInteractor.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        rayInteractor.selectEntered.RemoveListener(OnGrab);
        rayInteractor.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (lineVisual != null)
        {
            // Tắt tính năng ép tia Ray phải chạm vào vật
            lineVisual.stopLineAtSelection = false; 
            // Cắt ngắn tia Ray theo độ dài bạn muốn
            lineVisual.lineLength = shortLength; 
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (lineVisual != null)
        {
            // Trả lại mọi thứ như cũ khi thả tay
            lineVisual.stopLineAtSelection = originalStopAtSelection;
            lineVisual.lineLength = originalLength;
        }
    }
}