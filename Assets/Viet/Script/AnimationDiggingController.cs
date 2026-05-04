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

    // Biến để lưu lại tiếng xúc đất (vì nó là âm thanh lặp, cần lưu để tắt)
    private AudioSource diggingAudioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        
        // BẬT AUDIO XÚC ĐẤT VÀ LƯU LẠI
        if (AudioManager.Instance != null)
        {
            diggingAudioSource = AudioManager.Instance.PlaySound3D(SoundType.ExcavatorDig, transform.position);
        }

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
        {
          
            if (!anim.GetBool("Fall"))
            {
                anim.SetBool("Fall", true);

               
                if (AudioManager.Instance != null && diggingAudioSource != null)
                {
                    AudioManager.Instance.StopSound(diggingAudioSource);
                }

                
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySound3D(SoundType.PlayerFall, transform.position);
                }
            }
        }
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

        // BẬT LẠI ÂM THANH XÚC ĐẤT KHI RESET XONG
        // Check xem nếu âm thanh chưa phát thì mới gọi, tránh bị gọi đè 2 lần
        if (AudioManager.Instance != null)
        {
            if (diggingAudioSource == null || !diggingAudioSource.isPlaying)
            {
                diggingAudioSource = AudioManager.Instance.PlaySound3D(SoundType.ExcavatorDig, transform.position);
            }
        }
    }

    // Một biện pháp an toàn: Nếu object nhân vật này bị xóa khỏi scene (Destroy)
    // thì cũng phải tắt tiếng xúc đất đi, nếu không tiếng đào sẽ kêu mãi mãi ở tọa độ đó
    private void OnDestroy()
    {
        if (AudioManager.Instance != null && diggingAudioSource != null)
        {
            AudioManager.Instance.StopSound(diggingAudioSource);
        }
    }
}