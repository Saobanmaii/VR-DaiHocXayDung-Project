using UnityEngine;

// Gắn script này vào Object CHA chứa 3 lớp tường
public class ThreeWall : MonoBehaviour
{
    [Header("Kéo Material trong suốt dùng chung vào đây")]
    public Material transparentMaterial;

    [Header("Gán các lớp tường con tương ứng")]
    public Renderer outerWall;
    public Renderer middleWall;
    public Renderer innerWall;

    // Lưu trữ vật liệu gốc của từng lớp
    private Material[] originalOuterMats;
    private Material[] originalMiddleMats;
    private Material[] originalInnerMats;

    private bool isTransparent = false;

    void Start()
    {
        // Tự động lưu lại vật liệu gốc khi bắt đầu
        if (outerWall != null) originalOuterMats = outerWall.materials;
        if (middleWall != null) originalMiddleMats = middleWall.materials;
        if (innerWall != null) originalInnerMats = innerWall.materials;
    }

    // Hàm này sẽ được gọi bằng cách kéo thả trên Inspector của LỚP NGOÀI CÙNG
    public void ToggleAllWalls()
    {
        // Kiểm tra xem đã gán đủ các thành phần chưa
        if (outerWall == null || middleWall == null || innerWall == null || transparentMaterial == null)
        {
            Debug.LogError("Vui lòng gán đầy đủ Renderer và Material trong Inspector!");
            return;
        }

        if (isTransparent)
        {
            // Trả lại vật liệu gốc cho CẢ 3 lớp
            outerWall.materials = originalOuterMats;
            middleWall.materials = originalMiddleMats;
            innerWall.materials = originalInnerMats;
            isTransparent = false;
        }
        else
        {
            // Chuyển CẢ 3 lớp sang vật liệu trong suốt
            ApplyTransparentMaterial(outerWall);
            ApplyTransparentMaterial(middleWall);
            ApplyTransparentMaterial(innerWall);
            isTransparent = true;
        }
    }

    // Hàm phụ để đổi vật liệu một cách an toàn cho 1 object
    private void ApplyTransparentMaterial(Renderer rend)
    {
        Material[] newMaterials = new Material[rend.materials.Length];
        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = transparentMaterial;
        }
        rend.materials = newMaterials;
    }
}