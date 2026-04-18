using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVisibleUIEnchance : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            Debug.Log("here");
            LogicTrigger.instance.HienThiCanvasTrangBi(0);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
         LogicTrigger.instance.AnCanvasTrangBi();
    }
}   
