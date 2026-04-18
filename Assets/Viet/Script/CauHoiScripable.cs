using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName ="QandA",menuName ="QandA")]
public class CauHoiScripable : ScriptableObject
{
    public string TextQuestion;
    public List<string> TextListAnswer;
    public int correctAnwer;
}
