using UnityEngine;

public class CameraMapMovement : MonoBehaviour
{
    [Header("Mục tiêu theo dõi")]
    [Tooltip("Kéo XR Origin hoặc Main Camera của người chơi vào đây")]
    public Transform playerTarget;

    private float fixedYHeight;

    void Start()
    {
     
        fixedYHeight = transform.position.y;
    }

    void LateUpdate()
    {
        
        if (playerTarget != null)
        {
            
            Vector3 newPosition = new Vector3(playerTarget.position.x, fixedYHeight, playerTarget.position.z);

        
            transform.position = newPosition;
          
        }
    }
}