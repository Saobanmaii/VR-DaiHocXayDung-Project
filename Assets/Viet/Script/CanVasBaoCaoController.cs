using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanVasBaoCaoController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textWarning;



    public void SetUPTextWarning(string _text)
    {
        textWarning.text=_text;
    }
}
