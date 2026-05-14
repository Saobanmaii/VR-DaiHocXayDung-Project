using UnityEngine;

public class ErrorHighlight : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Color originalColor;
    public Color pulseColor = Color.red; // Màu khi nhấp nháy đến
    public float pulseSpeed = 2f;       // Tốc độ nhấp nháy

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            originalColor = meshRenderer.material.color;
        }
    }

    void Update()
    {
        if (meshRenderer == null) return;

        // Sử dụng hàm Sin để tạo giá trị dao động từ 0 đến 1
        float lerpTime = Mathf.PingPong(Time.time * pulseSpeed, 1f);

        // Đổi màu material dựa trên giá trị Sin
        meshRenderer.material.color = Color.Lerp(originalColor, pulseColor, lerpTime);
    }
}