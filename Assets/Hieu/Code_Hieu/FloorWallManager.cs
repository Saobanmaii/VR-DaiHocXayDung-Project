using UnityEngine;
using System.Collections.Generic;

public class FloorWallManager : MonoBehaviour
{
    public string floorTag = "Floor1";
    public Material transparentMaterial;

    private List<WallData> wallList = new List<WallData>();

    // Đã lưu sẵn ID của layer "Ignore Raycast" để code chạy nhanh hơn
    private int ignoreRaycastLayer;

    // Nâng cấp cấu trúc lưu trữ: Thêm biến lưu Layer gốc
    private class WallData
    {
        public GameObject wallObject; // Cần giữ GameObject để đổi Layer
        public Renderer renderer;
        public Material[] originalMaterials;
        public int originalLayer;     // Lưu Layer lúc ban đầu
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

    public void SetAllOpaque()
    {
        foreach (var wall in wallList)
        {
            // 1. Trả lại vật liệu
            wall.renderer.materials = wall.originalMaterials;
            // 2. Trả lại Layer gốc để tia Ray có thể chạm vào lại
            wall.wallObject.layer = wall.originalLayer;
        }
        Debug.Log("Đã làm ĐỤC và BẬT TƯƠNG TÁC tất cả tường tầng: " + floorTag);
    }

    public void SetAllTransparent()
    {
        foreach (var wall in wallList)
        {
            // 1. Đổi sang kính
            Material[] transMats = new Material[wall.originalMaterials.Length];
            for (int i = 0; i < transMats.Length; i++)
            {
                transMats[i] = transparentMaterial;
            }
            wall.renderer.materials = transMats;

            // 2. Ép sang layer Ignore Raycast để tia Ray xuyên qua
            wall.wallObject.layer = ignoreRaycastLayer;
        }
        Debug.Log("Đã làm TRONG và XUYÊN THẤU tất cả tường tầng: " + floorTag);
    }
}