
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LogicTrigger : MonoBehaviour
{
    public static LogicTrigger instance;
    public bool ThanhCongVaoCua=false;
    public bool check{get; private set;} 

    [Header("Gán các toggle vào đây")]
    [SerializeField] private List<Toggle> listToggle = new List<Toggle>();

    [Header("Gán các Canvas ở cổng vào đây")]
    [SerializeField] private List<GameObject> listCanvas= new List<GameObject>();

    public void Awake()
    {
        instance=this;
    }

    void Start()
    {
        SetActiveCanvasFalse();
    }

    public void checkLogic()
    {
        StringBuilder strb= new StringBuilder();
        if (!listToggle[0].isOn)
        {
            strb.AppendLine("Bạn quên chưa đội mũ");
            Debug.LogWarning("chưa đội mũ");
            
        }
        if (!listToggle[1].isOn)
        {
            strb.AppendLine("Bạn chưa mặc áo");
            Debug.LogWarning("chưa mặc áo");
        }
        if (!listToggle[2].isOn)
        {
            strb.AppendLine("Bạn chưa mặc giày");
            Debug.LogWarning("chưa mặc giày");
        }


        AnCanvasTrangBi(); // an canvas trang bi
        if (strb.Length>0)
        {
           
            listCanvas[2].SetActive(true);
            listCanvas[2].GetComponent<CanVasBaoCaoController>().SetUPTextWarning(strb.ToString());
        }
        else
        {
            strb.AppendLine("Đã trang bị đầy đủ, có thể vào giám sát công trình");
           
            listCanvas[3].SetActive(true);
            listCanvas[3].GetComponent<CanVasBaoCaoController>().SetUPTextWarning(strb.ToString());
            check=true;
            Debug.Log("Thanhf công qua cửa");
            ThanhCongVaoCua=true;
            OpenTheGateController.instance.OpenTheDoor();
            BlockPlayerController.instance.SetUnBlockBoxCollider();
            BlockRayCastController.setBlockUnBlock(true);
        }
    }

    void DoHide(float _time)
    {
        listCanvas[3].SetActive(false);
    }
    public void HienThiCanvasTrangBi(int idx)
    {
        SetActiveCanvasFalse();
        listCanvas[idx].SetActive(true);
       

    }

    public void AnCanvasTrangBi()
    {
        SetActiveCanvasFalse();

        

    }
    private void SetActiveCanvasFalse()
    {
        for (int i = 0; i < listCanvas.Count; i++)
        {

            listCanvas[i].SetActive(false);
        }
    }
}
