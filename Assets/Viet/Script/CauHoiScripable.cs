using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName ="QandA",menuName ="QandA")]
public class CauHoiScripable : ScriptableObject
{
    [TextArea(6,6)]
    public string TextQuestion;
    public string TextQuestion_EN;
    [TextArea(6,6)]
    public List<string> TextListAnswer;
    public List<string> TextListAnswer_EN;
    public int correctAnwer;
}
