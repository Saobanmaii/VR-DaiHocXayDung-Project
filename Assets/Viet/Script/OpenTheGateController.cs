using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class OpenTheGateController : MonoBehaviour
{
    public static OpenTheGateController instance;



    [Header("Gán cổng 1 vào đây")]
    [SerializeField] GameObject Cong1;
    [Header("Gán cổng 2 vào đây")]
    [SerializeField] GameObject Cong2;

    // Update is called once per frame
    void Awake() => instance=this;
    
    public void OpenTheDoor()
    {
        Cong1.transform.DOLocalRotate(new Vector3(-90  ,0,162),1f,RotateMode.Fast).SetEase(Ease.InOutSine);
        Cong2.transform.DOLocalRotate(new Vector3(-90  ,0,-162),1f,RotateMode.Fast).SetEase(Ease.InOutSine);
    }
}
