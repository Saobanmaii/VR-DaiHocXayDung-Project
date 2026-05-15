using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AlphaButton : MonoBehaviour
{
    void Start()
    {
       
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f; 
    }
}