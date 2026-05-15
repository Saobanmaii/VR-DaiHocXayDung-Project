using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName ="QandA",menuName ="QandA")]
public class CauHoiScripable : ScriptableObject
{
    [TextArea(6,6)]
    public string TextQuestion;
    [TextArea(6,6)]
    public List<string> TextListAnswer;
    public int correctAnwer;
}
