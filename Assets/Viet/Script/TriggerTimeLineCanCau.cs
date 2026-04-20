
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TriggerTimeLineCanCau : MonoBehaviour
{
    [SerializeField] PlayableDirector timeLine;
    [SerializeField] Canvas canvasGiaiThich;

    [Header("BlackHole")]
    public bool blockHoleCheck=false;
    [SerializeField] List<GameObject> invisibleGameobject;
    [SerializeField] List<GameObject> visibleGameobject;
    
    public void PlayTimeLine()
    {
        if(blockHoleCheck)
        {
            foreach(var hit in invisibleGameobject)
            {
                hit.SetActive(false);
            }
            foreach(var hit in visibleGameobject)
            {
                hit.SetActive(true);
            }
        }

        canvasGiaiThich.gameObject.SetActive(false);
        Invoke("VisibleCanvasGiaiThich",13f);
        timeLine.time = 0;
         timeLine.Play();

    }

    void VisibleCanvasGiaiThich()=>canvasGiaiThich.gameObject.SetActive(true);

    public void setUpVisible()
    {
        foreach(var hit in invisibleGameobject)
            {
                hit.SetActive(true);
            }
        foreach(var hit in visibleGameobject)
            {
                hit.SetActive(false);
            }
    }
    public void StopTimeLine() =>timeLine.Stop();
}
