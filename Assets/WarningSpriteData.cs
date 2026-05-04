using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WarningSpriteType
{
    electricShock,
    vapNga,
    xatLo,
    roiTuDo,
    chatDoc,
    vaChamXeCo,
    dutTay,
    beDotrenCao
}

[CreateAssetMenu(fileName = "WarningSpriteData", menuName = "ScriptableObjects/WarningSpriteData", order = 1)]
public class WarningSpriteData : ScriptableObject
{
    public WarningSpriteType _type;
    public Sprite sprite;
}
