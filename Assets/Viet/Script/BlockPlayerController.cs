using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayerController : MonoBehaviour
{
    public BoxCollider boxCollider;
    public static BlockPlayerController instance;
    [SerializeField] List<GameObject> blockPlayer; // Đối tượng dùng để chặn player


    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        boxCollider=GetComponent<BoxCollider>();
    }


    public void SetUnBlockBoxCollider()
    {
        foreach (GameObject block in blockPlayer)
        {
            block.SetActive(false);
        }
        boxCollider.gameObject.SetActive(false);
    }


}
