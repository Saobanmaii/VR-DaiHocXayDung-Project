using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    [Header("Cài đặt Bước chân")]
    public float stepDistance = 0.8f; 
    
    [Header("Chống dồn âm thanh (Teleport Fix)")]
    [Tooltip("Khoảng thời gian tối thiểu giữa 2 tiếng bước chân (giây)")]
    public float minTimeBetweenSteps = 0.3f; 
    
    [Tooltip("Khoảng cách tối đa trong 1 frame. Nếu lớn hơn số này sẽ tự hiểu là Teleport")]
    public float teleportThreshold = 1.5f; 

    private float distanceMoved = 0f;
    private Vector3 lastPosition;
    private float lastStepTime = 0f; // Lưu thời điểm phát âm thanh cuối cùng

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        
        Vector3 posLastNoY = new Vector3(lastPosition.x, 0, lastPosition.z);
        Vector3 posCurNoY = new Vector3(currentPosition.x, 0, currentPosition.z);

        float moveDelta = Vector3.Distance(posLastNoY, posCurNoY);

        // 1. NHẬN DIỆN TELEPORT
        // Nếu dịch chuyển một quãng quá xa trong 1 frame -> Chắc chắn là đang Teleport
        if (moveDelta > teleportThreshold)
        {
            distanceMoved = 0f; // Xóa sạch "nợ" khoảng cách
            lastPosition = currentPosition;
            return; // Dừng xử lý luôn ở frame này, không cộng dồn nữa
        }

        // 2. TÍNH TOÁN BƯỚC CHÂN BÌNH THƯỜNG
        if (moveDelta > 0.001f)
        {
            distanceMoved += moveDelta;
        }

        if (distanceMoved >= stepDistance)
        {
            // Ý TƯỞNG CỦA BẠN: Kiểm tra đã qua đủ thời gian giữa 2 bước chân chưa?
            if (Time.time - lastStepTime >= minTimeBetweenSteps)
            {
                PlayFootstepSound();
                lastStepTime = Time.time; // Cập nhật lại thời gian vừa phát
            }
            
            // Xóa khoảng cách đi để bắt đầu đếm lại bước mới
            // (Dùng = 0f thay vì -= để tránh tích tụ sai số khi giật lag)
            distanceMoved = 0f; 
        }

        lastPosition = currentPosition;
    }

    private void PlayFootstepSound()
    {
        Vector3 footPosition = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
        AudioManager.Instance.PlaySound3D(SoundType.PlayerMove, footPosition);
    }
}