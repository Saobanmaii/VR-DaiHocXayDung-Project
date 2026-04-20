using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayerController : MonoBehaviour
{
    public BoxCollider boxCollider;
    public static BlockPlayerController instance;


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
        boxCollider.enabled=false;
    }


}
