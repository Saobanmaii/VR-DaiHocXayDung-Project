using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTriggerControlelrXatLo : MonoBehaviour
{
    [SerializeField] LayerMask defaultLayer;
    [SerializeField] LayerMask steel;
    [SerializeField] List<GameObject> itemStatic;

    public void setSteelLayer()
    {
     
        int layerIndex = (int)Mathf.Log(steel.value, 2);

        foreach(var item in itemStatic)
        {
           
            if (item != null) 
            {
                item.layer = layerIndex;
            }
        }
    }

    public void setdefaultLayer()
    {
        
        int layerIndex = (int)Mathf.Log(defaultLayer.value, 2);

        foreach(var item in itemStatic)
        {
            if (item != null)
            {
                item.layer = layerIndex;
            }
        }
    }
}