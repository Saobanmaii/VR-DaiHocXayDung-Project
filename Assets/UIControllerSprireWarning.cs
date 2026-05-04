using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerSprireWarning : MonoBehaviour
{
    public WarningSpriteData data;
    public WarningSpriteType type;
    SpriteRenderer image;

    void Awake()
    {
        image = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    public void SetUp(WarningSpriteData data)
    {
        if (data != null && image != null)
        {
            type = data._type;
            image.sprite = data.sprite;
        }
    }
}
