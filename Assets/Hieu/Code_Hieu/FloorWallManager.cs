using UnityEngine;
using System.Collections.Generic;

public class FloorWallManager : MonoBehaviour
{
    public string floorTag = "Floor1";
    public Material transparentMaterial;

    private List<WallData> wallList = new List<WallData>();

    // Đã lưu sẵn ID của layer "Ignore Raycast" để code chạy nhanh hơn
    private int ignoreRaycastLayer;

    // THÊM: Biến kiểm tra trạng thái hiện tại (mặc định ban đầu là tường đục -> false)
    private bool isTransparent = false;

    // Nâng cấp cấu trúc lưu trữ: Thêm biến lưu Layer gốc
    private class WallData
    {
        public GameObject wallObject;
        public Renderer renderer;
        public Material[] originalMaterials;
        public int originalLayer;
    }

    void Start()
    {
        // Lấy ID chuẩn của layer Ignore Raycast trong Unity
        ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");

        GameObject[] walls = GameObject.FindGameObjectsWithTag(floorTag);
        foreach (GameObject wall in walls)
        {
            Renderer rend = wall.GetComponent<Renderer>();
            if (rend != null)
            {
                wallList.Add(new WallData
                {
                    wallObject = wall,
                    renderer = rend,
                    originalMaterials = rend.materials,
                    originalLayer = wall.layer // Ghi nhớ layer gốc (VD: Default)
                });
            }
        }
    }

    // HÀM MỚI: Dùng cho 1 nút bấm duy nhất (Toggle)
    public void ToggleWallState()
    {
        // Đảo ngược trạng thái hiện tại (Đục -> Trong, Trong -> Đục)
        isTransparent = !isTransparent;

        if (isTransparent)
        {
            // --- XỬ LÝ LÀM TRONG TƯỜNG ---
            foreach (var wall in wallList)
            {
                Material[] transMats = new Material[wall.originalMaterials.Length];
                for (int i = 0; i < transMats.Length; i++)
                {
                    transMats[i] = transparentMaterial;
                }
                wall.renderer.materials = transMats;

                // Ép sang layer Ignore Raycast để tia Ray xuyên qua
                wall.wallObject.layer = ignoreRaycastLayer;
            }
            Debug.Log("Đã làm TRONG và XUYÊN THẤU tất cả tường tầng: " + floorTag);
        }
        else
        {
            // --- XỬ LÝ LÀM ĐỤC TƯỜNG ---
            foreach (var wall in wallList)
            {
                // Trả lại vật liệu
                wall.renderer.materials = wall.originalMaterials;
                // Trả lại Layer gốc để tia Ray có thể chạm vào lại
                wall.wallObject.layer = wall.originalLayer;
            }
            Debug.Log("Đã làm ĐỤC và BẬT TƯƠNG TÁC tất cả tường tầng: " + floorTag);
        }
    }
}