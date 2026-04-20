using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDiggingController : MonoBehaviour
{
    Animator anim;
    public List<GameObject> listSteels; 
    
    
    private struct Pose {
        public Vector3 position;
        public Quaternion rotation;
    }
    
    private List<Pose> initialPoses = new List<Pose>();

    void Start()
    {
        anim = GetComponent<Animator>();
        
        foreach(var x in listSteels)
        {
           
            initialPoses.Add(new Pose { 
                position = x.transform.position, 
                rotation = x.transform.rotation 
            });
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Steel"))
            anim.SetBool("Fall", true);
    }

    public void setupBegin()
    {
        anim.SetBool("Fall", false);

        for(int i = 0; i < listSteels.Count; i++)
        {
            Rigidbody rb = listSteels[i].GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;

               
                listSteels[i].transform.position = initialPoses[i].position;
                listSteels[i].transform.rotation = initialPoses[i].rotation;

               
                

                
                rb.isKinematic = false;
            }
        }
        
        Physics.SyncTransforms();
    }
}