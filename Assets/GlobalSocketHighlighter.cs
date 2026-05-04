using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class GlobalSocketHighlighter : MonoBehaviour
{
    public List<GameObject> socketsHightLighted = new List<GameObject>();

   public void HighlightSockets()
    {
        foreach (GameObject socket in socketsHightLighted)
        {
            if(socket.activeSelf) continue; 
            socket.SetActive(true);
        }
    }

    public void UnHighlightSockets()
    {
        Debug.Log("Sự kiện Select Entered đã kích hoạt hàm này!"); 
        foreach (GameObject socket in socketsHightLighted)
        {
    
            socket.SetActive(false);
        }
    }

}