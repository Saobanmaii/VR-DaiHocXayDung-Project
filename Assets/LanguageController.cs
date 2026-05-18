using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{
    public static LanguageController instance;
    [Header("hướng dẫn")]
    public bool switchVN = true;
    

    #region hướng dẫn canvas
    [Header("hướng dẫn canvas")]
    public TextMeshProUGUI headerHuongDanCanvas;
    public string headerHuongDanCanvas_VN;
    public string headerHuongDanCanvas_EN;
    public List<TextMeshProUGUI> textHuongDanCanvas = new List<TextMeshProUGUI>();
    [TextArea(3, 2)]
    public List<string> languagesHuongDanCanvas_VN = new List<string>();
    [TextArea(3, 2)]
    public List<string> languagesHuongDanCanvas_EN = new List<string>();
    #endregion
    
    #region  vui lòng trang bị canvas
    [Space(10)]

    [Header("vui long trang bij canvas")]
    public TextMeshProUGUI textVuiLongTrangBiCanvas;
    public string textVuiLongTrangBiCanvas_VN;
    public string textVuiLongTrangBiCanvas_EN;

    public TextMeshProUGUI textButtonVuiLongTrangBiCanvas;
    public string textButtonVuiLongTrangBiCanvas_VN;
    public string textButtonVuiLongTrangBiCanvas_EN;
    
    [Space(10)]
    public TextMeshProUGUI textChuaTrangBiCanvasWarning;
    public string textChuaTrangBiCanvas_VN;
    public string textChuaTrangBiCanvas_EN;
#endregion
#region  trang bi canvas
    [Space(10)]
    [Header("TrangBi Canvas")]
    public TextMeshProUGUI TrangbiCanvasHeader;
    public string TrangbiCanvasHeader_VN;
    public string TrangbiCanvasHeader_EN;
    
    public List<TextMeshProUGUI> textTrangBiCanvasOption = new List<TextMeshProUGUI>();
    public List<string> textTrangBiCanvasOption_VN = new List<string>();
    public List<string> textTrangBiCanvasOption_EN = new List<string>();

    public TextMeshProUGUI textTrangBiCanvasButton;
    public string textTrangBiCanvasButton_VN;  
    public string textTrangBiCanvasButton_EN;
#endregion
#region text cảnh báo thiếu đồ
    [Header("Text cảnh báo thiếu đồ")]
    public String warningNoPPE_VN_Hat;
    public String warningNoPPE_EN_Hat;
    public String warningNoPPE_VN_Clothes;
    public String warningNoPPE_EN_Clothes;
    public String warningNoPPE_VN_Shoes;
    public String warningNoPPE_EN_Shoes;
    
    [Header("Text thông báo vào cổng thành công")]
    [HideInInspector]
    public String successEnter_VN = "Đã trang bị đầy đủ, có thể vào giám sát công trình";
    [HideInInspector]
    public String successEnter_EN = "Fully equipped, ready to enter the construction site";
#endregion
#region  textMenu
    [Space(10)]
    [Header("Text menu")]
    public TextMeshProUGUI canvasMenu;
    public string canvasMenu_VN;
    public string canvasMenu_EN;
    public TextMeshProUGUI canvsSetting;
    public string canvasSetting_VN;
    public string canvasSetting_EN;
    public TextMeshProUGUI canvasMap;
    public string canvasMap_VN;
    public string canvasMap_EN;
#endregion
#region text setting menu canvas
    [Header("Text setting menu canvas")]
    public TextMeshProUGUI textSettingMenuCanvasHeader;
    public string textSettingMenuCanvasHeader_VN;
    public string textSettingMenuCanvasHeader_EN;

    public TextMeshProUGUI textSoundSetting;
    public string textSoundSetting_VN;
    public string textSoundSetting_EN;
    public TextMeshProUGUI textLightSetting;
    public string textLightSetting_VN;
    public string textLightSetting_EN;
#endregion
#region text setting map cavas
        [Header("Text setting map canvas")]
        public TextMeshProUGUI textSettingMapCanvasHeader;
        public string textSettingMapCanvasHeader_VN;
        public string textSettingMapCanvasHeader_EN;
        public TextMeshProUGUI thuPhongTextMeshPro;
        public string thuPhongTextMeshPro_VN;
        public string thuPhongTextMeshPro_EN;
#endregion
#region text MainMenuCanvas
[Space(10)]
[Header("Text MainMenuCanvas")]
public TextMeshProUGUI textMainMenuCanvasHeader;
public string textMainMenuCanvasHeader_VN;
public string textMainMenuCanvasHeader_EN;

public TextMeshProUGUI textMainMenuCanvasHomeButton;
public string textMainMenuCanvasHomeButton_VN;
public string textMainMenuCanvasHomeButton_EN;
public TextMeshProUGUI textMainMenuCanvasPlayeAgainButton;
public string textMainMenuCanvasPlayAgainButton_VN;
public string textMainMenuCanvasPlayAgainButton_EN;
public TextMeshProUGUI textMainMenuCanvasExitButton;
public string textMainMenuCanvasExitButton_VN;
public string textMainMenuCanvasExitButton_EN;
#endregion

#region ElectrickSockCanvas
[Space(10)]
[Header("Text ElectrickSockCanvas")]
public TextMeshProUGUI textElectrickSockCanvasHeader;
public string textElectrickSockCanvasHeader_VN;
public string textElectrickSockCanvasHeader_EN;

public TextMeshProUGUI textElectrickExplain;
public string textElectrickExplain_VN;
public string textElectrickExplain_EN;

public TextMeshProUGUI textElectrickButton;
public string textElectrickButton_VN;
public string textElectrickButton_EN;
#endregion
#region SandslideCanVas
    [Space(10)]
    [Header("Text SandslideCanvas")]
    public TextMeshProUGUI textSandslideCanvasHeader;
    public string textSandslideCanvasHeader_VN;
    public string textSandslideCanvasHeader_EN;
    public TextMeshProUGUI textSandslideExplain;
    public string textSandslideExplain_VN;
    public string textSandslideExplain_EN;
    public TextMeshProUGUI textSandslideButton;
    public string textSandslideButton_VN;
    public string textSandslideButton_EN;
#endregion

#region CanCauCanvas
    [Space(10)]
    [Header("Text CanCauCanvas")]
    public TextMeshProUGUI textCanCauCanvasHeader;
    public string textCanCauCanvasHeader_VN;
    public string textCanCauCanvasHeader_EN;
    public TextMeshProUGUI textCanCauExplain;
    public string textCanCauExplain_VN;
    public string textCanCauExplain_EN;
    public TextMeshProUGUI textCanCauButton;
    public string textCanCauButton_VN;
    public string textCanCauButton_EN;
#endregion
#region SanSauCanvasExplain
    [Space(10)]
    [Header("Text SanSauCanvas")]
    public TextMeshProUGUI textSanSauCanvasHeader;
    public string textSanSauCanvasHeader_VN;
    public string textSanSauCanvasHeader_EN;
    public TextMeshProUGUI textSanSauExplain;
    public string textSanSauExplain_VN;
    public string textSanSauExplain_EN;
    public TextMeshProUGUI textSanSauButton;
    public string textSanSauButton_VN;
    public string textSanSauButton_EN;
#endregion
#region ScrollBarriel
    [Space(10)]
    [Header("Text ScrollBarriel")]
    public TextMeshProUGUI textScrollBarrielHeader;
    public String textScrollBarrielHeader_VN;
    public String textScrollBarrielHeader_EN; 
    public TextMeshProUGUI textScrollBarrielButton;
    public String textScrollBarrielButton_VN;
    public String textScrollBarrielButton_EN;
#endregion
#region canvasPickWrongBarrier
    [Space(30)]
    [Header("Text Pick Wrong Barrier")]
    public List<TextMeshProUGUI> textPickWrongBarrierHeader;
    public List<string> textPickWrongBarrierHeader_VN = new List<string>();
    public List<string> textPickWrongBarrierHeader_EN = new List<string>();

    public List<TextMeshProUGUI> textPickWrongBarrierButton;
    public List<string> textPickWrongBarrierButton_VN = new List<string>();
    public List<string> textPickWrongBarrierButton_EN = new List<string>();


    public List<TextMeshProUGUI> textPickWrongBarrierExplain;
    public List<string> textPickWrongBarrierExplain_VN = new List<string>();
    public List<string> textPickWrongBarrierExplain_EN = new List<string>();
#endregion
#region vipham khoang cach an toan
    [Space(30)]
    [Header("Text Vi Pham Khoang Cach An Toan")]
    public TextMeshProUGUI textViPhamKhoangCachAnToan;
    public String textViPhamKhoangCachAnToan_VN;
    public String textViPhamKhoangCachAnToan_EN;
#endregion

    void Awake()
    {
        instance = this;
            

    }
    
    void Start()
    {
        SwitchInstructionStart();
        SwithCanvasVuiLongTrangBiCanvas();
        SwitchChuaTrangBiCanvasWarning();
        SwitchTrangBiCanvas();
        SwitchMenuCanvas();
        SwitchSettingCanvas();
        SwitchMapSetting();
        SwitchMainMenu();
        SwitchElectricCanvas();
        SwitchSandslideCanvas();
        SwitchCanCauCanvas();
        SwitchSanSauCanvas();
        SwitchScrollBarriel();

        SwitchPickWrongBarrierCanvas();
        switchViPhamKhoangCachAnToan();
    }

    private void switchViPhamKhoangCachAnToan()
    {
        if(switchVN)
        {
            textViPhamKhoangCachAnToan.text = textViPhamKhoangCachAnToan_VN;
        }
        else
        {
            textViPhamKhoangCachAnToan.text = textViPhamKhoangCachAnToan_EN;
        }
    }

    private void SwitchPickWrongBarrierCanvas()
    {
       if(switchVN)
       {
           for (int i = 0; i < textPickWrongBarrierHeader.Count; i++)
           {
               textPickWrongBarrierHeader[i].text = textPickWrongBarrierHeader_VN[i];
           }
           for (int i = 0; i < textPickWrongBarrierButton.Count; i++)
           {
               textPickWrongBarrierButton[i].text = textPickWrongBarrierButton_VN[i];
           }
           for (int i = 0; i < textPickWrongBarrierExplain.Count; i++)
           {
               textPickWrongBarrierExplain[i].text = textPickWrongBarrierExplain_VN[i];
           }
       }
       else
       {
           for (int i = 0; i < textPickWrongBarrierHeader.Count; i++)
           {
               textPickWrongBarrierHeader[i].text = textPickWrongBarrierHeader_EN[i];
           }
           for (int i = 0; i < textPickWrongBarrierButton.Count; i++)
           {
               textPickWrongBarrierButton[i].text = textPickWrongBarrierButton_EN[i];
           }
           for (int i = 0; i < textPickWrongBarrierExplain.Count; i++)
           {
               textPickWrongBarrierExplain[i].text = textPickWrongBarrierExplain_EN[i];
           }
       }
    }

    private void SwitchScrollBarriel()
    {
        if(switchVN)
        {
            textScrollBarrielHeader.text = textScrollBarrielHeader_VN;
            textScrollBarrielButton.text = textScrollBarrielButton_VN;
        }
        else
        {
            textScrollBarrielHeader.text = textScrollBarrielHeader_EN;
            textScrollBarrielButton.text = textScrollBarrielButton_EN;
        }
    }

    private void SwitchSanSauCanvas()
    {
        if (switchVN)
        {
            textSanSauCanvasHeader.text = textSanSauCanvasHeader_VN;
            textSanSauExplain.text = textSanSauExplain_VN;
            textSanSauButton.text = textSanSauButton_VN;

        }
        else
        {
            textSanSauCanvasHeader.text = textSanSauCanvasHeader_EN;
            textSanSauExplain.text = textSanSauExplain_EN;
            textSanSauButton.text = textSanSauButton_EN;
        }
    }

    private void SwitchCanCauCanvas()
    {
        if(switchVN)
        {
            textCanCauCanvasHeader.text = textCanCauCanvasHeader_VN;
            textCanCauExplain.text = textCanCauExplain_VN;
            textCanCauButton.text = textCanCauButton_VN;
        }
        else
        {
            textCanCauCanvasHeader.text = textCanCauCanvasHeader_EN;
            textCanCauExplain.text = textCanCauExplain_EN;
            textCanCauButton.text = textCanCauButton_EN;
        }
    }

    private void SwitchSandslideCanvas()
    {
       if(switchVN)
       {
           textSandslideCanvasHeader.text = textSandslideCanvasHeader_VN;
           textSandslideExplain.text = textSandslideExplain_VN;
           textSandslideButton.text = textSandslideButton_VN;
       }
       else
       {
           textSandslideCanvasHeader.text = textSandslideCanvasHeader_EN;
           textSandslideExplain.text = textSandslideExplain_EN;
           textSandslideButton.text = textSandslideButton_EN;
       }
    }

    private void SwitchElectricCanvas()
    {
        if (switchVN)
        {
            textElectrickSockCanvasHeader.text = textElectrickSockCanvasHeader_VN;
            textElectrickExplain.text = textElectrickExplain_VN;
            textElectrickButton.text = textElectrickButton_VN;
        }
        else
        {
            textElectrickSockCanvasHeader.text = textElectrickSockCanvasHeader_EN;
            textElectrickExplain.text = textElectrickExplain_EN;
            textElectrickButton.text = textElectrickButton_EN;
        }
    }

    private void SwitchMainMenu()
    {
        if (switchVN)
        {
            textMainMenuCanvasHeader.text = textMainMenuCanvasHeader_VN;
            textMainMenuCanvasHomeButton.text = textMainMenuCanvasHomeButton_VN;
            textMainMenuCanvasPlayeAgainButton.text = textMainMenuCanvasPlayAgainButton_VN;
            textMainMenuCanvasExitButton.text = textMainMenuCanvasExitButton_VN;
        }
        else
        {
            textMainMenuCanvasHeader.text = textMainMenuCanvasHeader_EN;
            textMainMenuCanvasHomeButton.text = textMainMenuCanvasHomeButton_EN;
            textMainMenuCanvasPlayeAgainButton.text = textMainMenuCanvasPlayAgainButton_EN;
            textMainMenuCanvasExitButton.text = textMainMenuCanvasExitButton_EN;
        }
    }

    private void SwitchMapSetting()
    {
        if (switchVN)
        {
            textSettingMapCanvasHeader.text = textSettingMapCanvasHeader_VN;
            thuPhongTextMeshPro.text = thuPhongTextMeshPro_VN;

        }
        else
        {
            textSettingMapCanvasHeader.text = textSettingMapCanvasHeader_EN;
            thuPhongTextMeshPro.text = thuPhongTextMeshPro_EN;
        }
    }

    void SwitchSettingCanvas()
    {
        if (switchVN)
        {
            textSettingMenuCanvasHeader.text = textSettingMenuCanvasHeader_VN;
            textSoundSetting.text = textSoundSetting_VN;
            textLightSetting.text = textLightSetting_VN;
        }
        else
        {
            textSettingMenuCanvasHeader.text = textSettingMenuCanvasHeader_EN;
            textSoundSetting.text = textSoundSetting_EN;
            textLightSetting.text = textLightSetting_EN;
        }
    }

    private void SwitchMenuCanvas()
    {
        if (switchVN)
        {
            canvasMenu.text = canvasMenu_VN;
            canvsSetting.text = canvasSetting_VN;
            canvasMap.text = canvasMap_VN;
        }
        else
        {
            canvasMenu.text = canvasMenu_EN;
            canvsSetting.text = canvasSetting_EN;
            canvasMap.text = canvasMap_EN;
        }
    }

    void SwitchTrangBiCanvas()
    {
        if (switchVN)
        {
            TrangbiCanvasHeader.text = TrangbiCanvasHeader_VN;
            for (int i = 0; i < textTrangBiCanvasOption.Count; i++)
            {
                textTrangBiCanvasOption[i].text = textTrangBiCanvasOption_VN[i];
            }
            textTrangBiCanvasButton.text = textTrangBiCanvasButton_VN;
        }
        else
        {
            TrangbiCanvasHeader.text = TrangbiCanvasHeader_EN;
            for (int i = 0; i < textTrangBiCanvasOption.Count; i++)
            {
                textTrangBiCanvasOption[i].text = textTrangBiCanvasOption_EN[i];
            }
            textTrangBiCanvasButton.text = textTrangBiCanvasButton_EN;
        }
    }
    private void SwitchChuaTrangBiCanvasWarning()
    {
        if (switchVN)
        {
            textChuaTrangBiCanvasWarning.text = textChuaTrangBiCanvas_VN;
        }
        else
        {
            textChuaTrangBiCanvasWarning.text = textChuaTrangBiCanvas_EN;
        }
    }

    private void SwithCanvasVuiLongTrangBiCanvas()
    {
        if (switchVN)
        {
            textVuiLongTrangBiCanvas.text = textVuiLongTrangBiCanvas_VN;
            textButtonVuiLongTrangBiCanvas.text = textButtonVuiLongTrangBiCanvas_VN;
        }
        else
        {
            textVuiLongTrangBiCanvas.text = textVuiLongTrangBiCanvas_EN;
            textButtonVuiLongTrangBiCanvas.text = textButtonVuiLongTrangBiCanvas_EN;
        }
    }

    private void SwitchInstructionStart()
    {
        if (switchVN)
        {
            headerHuongDanCanvas.text = headerHuongDanCanvas_VN;
            for (int i = 0; i < languagesHuongDanCanvas_VN.Count; i++)
            {
                textHuongDanCanvas[i].text = languagesHuongDanCanvas_VN[i];
            }
        }
        else
        {
            headerHuongDanCanvas.text = headerHuongDanCanvas_EN;
            for (int i = 0; i < languagesHuongDanCanvas_EN.Count; i++)
            {
                textHuongDanCanvas[i].text = languagesHuongDanCanvas_EN[i];
            }
        }
    }
}