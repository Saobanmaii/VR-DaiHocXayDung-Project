using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CurvedUIEffect : BaseMeshEffect
{
    [Tooltip("Bán kính độ cong. Số càng nhỏ càng cong mạnh.")]
    public float radius = 500f;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive() || vh.currentVertCount == 0 || radius == 0) return;

        UIVertex vertex = new UIVertex();
        for (int i = 0; i < vh.currentVertCount; i++)
        {
            vh.PopulateUIVertex(ref vertex, i);

           
            float theta = vertex.position.x / radius;

            
            vertex.position.z -= radius * (1 - Mathf.Cos(theta));
            vertex.position.x = radius * Mathf.Sin(theta);

            vh.SetUIVertex(vertex, i);
        }
    }
}