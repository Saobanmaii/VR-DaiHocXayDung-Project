using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Bắt buộc phải có cái này

// Kế thừa từ XRSimpleInteractable thay vì MonoBehaviour
public class XRWallTransPlus : XRSimpleInteractable
{
    [Header("Material muốn dùng khi tàng hình")]
    public Material transparentMaterial;

    private Renderer meshRenderer;
    private int ignoreRaycastLayer;

    // Dùng Awake để thiết lập thông số ngay từ lúc game chưa chạy
    protected override void Awake()
    {
        base.Awake(); // Bắt buộc gọi base.Awake() để hệ thống XR khởi tạo

        meshRenderer = GetComponent<Renderer>();

        // Bạn có thể đổi chữ "Ignore Raycast" thành "TuongTangHinh" nếu vẫn đang dùng Layer tự chế hôm trước nhé
        ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
    }

    // Hàm này tự động được gọi khi bạn bóp cò (Select) tia VR vào bức tường này
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args); // Gọi base để hệ thống ghi nhận event

        MakeTransparent();
    }

    private void MakeTransparent()
    {
        if (meshRenderer == null || transparentMaterial == null) return;

        // 1. Đổi sang tường trong suốt
        Material[] newMaterials = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = transparentMaterial;
        }
        meshRenderer.materials = newMaterials;

        // 2. Đổi Layer thành Ignore Raycast để tia lọt qua (đồng nghĩa với việc không click lại được nữa)
        gameObject.layer = ignoreRaycastLayer;
    }
}