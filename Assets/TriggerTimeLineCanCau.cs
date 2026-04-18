
using UnityEngine;
using UnityEngine.Playables;


public class TriggerTimeLineCanCau : MonoBehaviour
{
    [SerializeField] PlayableDirector timeLine;
    [SerializeField] Canvas canvasGiaiThich;
    
    public void PlayTimeLine()
    {
        canvasGiaiThich.gameObject.SetActive(false);
        Invoke("VisibleCanvasGiaiThich",13f);
        timeLine.time = 0;
         timeLine.Play();
    }

    void VisibleCanvasGiaiThich()=>canvasGiaiThich.gameObject.SetActive(true);

    public void StopTimeLine() =>timeLine.Stop();
}
