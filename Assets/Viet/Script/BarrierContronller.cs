

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierContronller : MonoBehaviour
{
    public List<Image>images;
    public List<GameObject>barrior;

    public List<GameObject>listCanvasBarrior;
    public int _idx=1;

    void Start()
    {
        SetVisibleBarrior();
        PickPicture(_idx);
    }

    private void SetVisibleBarrior()
    {
        foreach (var hit in barrior)
        {
            hit.SetActive(false);
        }
    }

     public void SetInVisibleBarriorCanvas()
    {
        foreach (var hit in listCanvasBarrior)
        {
            hit.SetActive(false);
        }
    }

    public void PickPicture(int idx)
    {
        smallAllCanvas();
        _idx=idx;
         images[idx].rectTransform.localScale=Vector3.one*1.2f;
         PickBarrier(idx);
         Debug.Log("Đã chọn pickture thứ: "+idx);
    }

    public void PickUpCanvasBarrier()
    {
        SetInVisibleBarriorCanvas();
        listCanvasBarrior[_idx].SetActive(true);
    }

    void smallAllCanvas()
    {
        for(int i = 0; i < images.Count; i++)
        {
            images[i].rectTransform.localScale=Vector3.one;
        }
    }

    public void PickBarrier(int idx)
    {
        SetVisibleBarrior();
        barrior[idx].SetActive(true);
    } 
}
