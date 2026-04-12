using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    [Header("Kéo Material trong suốt vào đây")]
    public Material transparentMaterial;

    private Material[] originalMaterials;
    private Renderer meshRenderer;
    private bool isTransparent = false;

    void Start()
    {
        // Tự động lấy Renderer và lưu lại Material gốc ngay khi bắt đầu
        meshRenderer = GetComponent<Renderer>();
        if (meshRenderer != null)
        {
            originalMaterials = meshRenderer.materials;
        }
    }

    // Hàm này sẽ được gọi bằng cách kéo thả trên Inspector
    public void ToggleMaterial()
    {
        if (meshRenderer == null) return;

        if (isTransparent)
        {
            // Trả lại tường đặc
            meshRenderer.materials = originalMaterials;
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
            isTransparent = true;
        }
    }
}