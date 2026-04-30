using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    [Header("Material want to use")]
    public Material transparentMaterial;

    private Material[] originalMaterials;
    private Renderer meshRenderer;
    private bool isTransparent = false;

    // [THÊM 1]: Biến để nhớ Layer lúc đầu của bức tường
    private int originalLayer;

    void Start()
    {
        // Tự động lấy Renderer và lưu lại Material gốc ngay khi bắt đầu
        meshRenderer = GetComponent<Renderer>();
        if (meshRenderer != null)
        {
            originalMaterials = meshRenderer.materials;
        }

        // [THÊM 2]: Ghi nhớ Layer gốc trước khi đổi
        originalLayer = gameObject.layer;
    }

    // Hàm này sẽ được gọi bằng cách kéo thả trên Inspector
    public void ToggleMaterial()
    {
        if (meshRenderer == null) return;

        if (isTransparent)
        {
            // Trả lại tường đặc
            meshRenderer.materials = originalMaterials;

            // [THÊM 3a]: Trả lại Layer gốc ban đầu
            gameObject.layer = originalLayer;

            isTransparent = false;
        }
        else
        {
            // Đổi sang tường trong suốt
            Material[] newMaterials = new Material[originalMaterials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = transparentMaterial;
            }
            meshRenderer.materials = newMaterials;

            // [THÊM 3b]: Đổi Layer thành Ignore Raycast để tia lọt qua
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

            isTransparent = true;
        }
    }
}