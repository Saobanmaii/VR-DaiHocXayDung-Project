using UnityEngine;

public class goBack : MonoBehaviour
{
    [Header("Kéo cục gốc XR Rig của người chơi vào đây")]
    public Transform playerTransform;

    [Header("Khoảng cách đẩy lùi (mét)")]
    public float khoangCachLui = 2f;

    // Bất cứ vật gì có Rigidbody + Collider chạm vào Trigger đều kích hoạt
    private void OnTriggerEnter(Collider other)
    {
        if (playerTransform != null)
        {
            // Lấy vị trí hiện tại của người chơi
            Vector3 viTriHienTai = playerTransform.position;

            // Tính toán vị trí mới: Lùi lại phía sau lưng người chơi 2 mét
            // (playerTransform.forward là hướng nhìn tới trước, trừ đi nó sẽ là lùi lại)
            Vector3 viTriLui = viTriHienTai - (playerTransform.forward * khoangCachLui);

            // Dịch chuyển người chơi ngay lập tức về vị trí lùi
            playerTransform.position = viTriLui;
        }
        else
        {
            Debug.LogWarning("Chưa kéo XR Rig vào ô Player Transform kìa a ơi!");
        }
    }
}