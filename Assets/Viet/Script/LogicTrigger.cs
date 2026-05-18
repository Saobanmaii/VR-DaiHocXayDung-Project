using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LogicTrigger : MonoBehaviour
{
    public static LogicTrigger instance;
    public bool ThanhCongVaoCua = false;
    public bool check { get; private set; } 

    [Header("Gán các toggle vào đây")]
    [SerializeField] private List<Toggle> listToggle = new List<Toggle>();

    [Header("Gán các Canvas ở cổng vào đây")]
    [SerializeField] private List<GameObject> listCanvas = new List<GameObject>();

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetActiveCanvasFalse();
        listCanvas[0].SetActive(true);
    }

    public void checkLogic()
    {
        StringBuilder strb = new StringBuilder();
        
        // Lấy trạng thái ngôn ngữ hiện tại
        bool isVN = LanguageController.instance.switchVN; 

        // Kiểm tra từng món đồ và nối chuỗi tương ứng với ngôn ngữ
        if (!listToggle[0].isOn)
        {
            strb.AppendLine(isVN ? LanguageController.instance.warningNoPPE_VN_Hat : LanguageController.instance.warningNoPPE_EN_Hat);
            Debug.LogWarning("chưa đội mũ");
        }
        if (!listToggle[1].isOn)
        {
            strb.AppendLine(isVN ? LanguageController.instance.warningNoPPE_VN_Clothes : LanguageController.instance.warningNoPPE_EN_Clothes);
            Debug.LogWarning("chưa mặc áo");
        }
        if (!listToggle[2].isOn)
        {
            strb.AppendLine(isVN ? LanguageController.instance.warningNoPPE_VN_Shoes : LanguageController.instance.warningNoPPE_EN_Shoes);
            Debug.LogWarning("chưa mặc giày");
        }

        AnCanvasTrangBi(); // an canvas trang bi
        
        // Nếu StringBuilder có dữ liệu -> Thiếu đồ
        if (strb.Length > 0)
        {
            listCanvas[2].SetActive(true);
            
            // ĐÃ HOÀN THIỆN: Gọi âm thanh cảnh báo thiếu đồ (phát 2D, tự động tắt khi hết clip)
            AudioManager.Instance.PlaySound2D(SoundType.WarningNoPPE); 
            
            listCanvas[2].GetComponent<CanVasBaoCaoController>().SetUPTextWarning(strb.ToString());
        }
        else // Đã đủ đồ
        {
            // Lấy câu thông báo thành công theo ngôn ngữ
            strb.AppendLine(isVN ? LanguageController.instance.successEnter_VN : LanguageController.instance.successEnter_EN);
            
            listCanvas[3].SetActive(true);
            listCanvas[3].GetComponent<CanVasBaoCaoController>().SetUPTextWarning(strb.ToString());
            check = true;
            Debug.Log("Thành công qua cửa");
            ThanhCongVaoCua = true;

            // ĐỀ XUẤT THÊM: Phát âm thanh báo hiệu kiểm tra thành công (Ví dụ tiếng Ting Ting)
            AudioManager.Instance.PlaySound2D(SoundType.UI_Correct);

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