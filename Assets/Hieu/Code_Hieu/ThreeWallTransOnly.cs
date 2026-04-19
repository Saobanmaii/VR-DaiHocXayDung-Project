using UnityEngine;

public class ThreeWallTransOnly : MonoBehaviour
{
    public Material transparentMaterial;
    public Renderer outerWall;
    public Renderer middleWall;
    public Renderer innerWall;

    private int ignoreLayer;

    void Start()
    {
        ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public void MakeWallsTransparent()
    {
        // Đã xóa dòng if (isTransparent) return; ở đây

        if (outerWall == null || middleWall == null || innerWall == null || transparentMaterial == null)
        {
            Debug.LogError("Missing Renderer or Material references.");
            return;
        }

        ApplyTransparencyAndLayer(outerWall);
        ApplyTransparencyAndLayer(middleWall);
        ApplyTransparencyAndLayer(innerWall);
    }

    private void ApplyTransparencyAndLayer(Renderer rend)
    {
        Material[] newMaterials = new Material[rend.materials.Length];
        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = transparentMaterial;
        }
        rend.materials = newMaterials;
        rend.gameObject.layer = ignoreLayer;
    }
}