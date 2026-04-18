using UnityEngine;

public class VRBillboard : MonoBehaviour
{
    
    public Transform playerCamera;
    public bool nguoc=true;
    
    public bool onlyYaw = true;

    void Update()
    {
        if (playerCamera != null)
        {
            if (onlyYaw)
            {
               
                Vector3 lookPos = playerCamera.position;
                lookPos.y = transform.position.y; 
                transform.LookAt(lookPos);
                
              
            }
            else
            {
                
                transform.LookAt(playerCamera);
            }
        if(nguoc)
            transform.Rotate(0, 180f, 0);
        }
    }
}