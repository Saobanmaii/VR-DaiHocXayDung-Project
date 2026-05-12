using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class UIBIM : MonoBehaviour
{
    [Header("Cấu hình XR Ray")]
    public XRRayInteractor xrRay;

    [Header("Cấu hình UI")]
    public GameObject uiCanvas;
    public TextMeshProUGUI infoText;

    // Biến lưu GUID để làm tính năng click 2 lần tắt
    private string currentGUID = "";

    void OnEnable()
    {
        if (xrRay != null) xrRay.selectEntered.AddListener(OnObjectClicked);
    }

    void OnDisable()
    {
        if (xrRay != null) xrRay.selectEntered.RemoveListener(OnObjectClicked);
    }

    private void OnObjectClicked(SelectEnterEventArgs args)
    {
        BIMElement bim = args.interactableObject.transform.GetComponent<BIMElement>();
        if (bim != null)
        {
            // TÍNH NĂNG 1: CLICK LẦN 2 ĐỂ TẮT
            if (uiCanvas.activeSelf && currentGUID == bim.GUID)
            {
                uiCanvas.SetActive(false);
                currentGUID = "";
                return;
            }

            BIMData data = BIMDatabase.Get(bim.GUID);
            if (data != null)
            {
                currentGUID = bim.GUID;

                if (xrRay.TryGetCurrent3DRaycastHit(out RaycastHit hit))
                {
                    ShowUIAtPoint(hit.point, hit.normal, data);
                }
                else
                {
                    ShowUIAtPoint(args.interactableObject.transform.position, Vector3.up, data);
                }
            }
        }
    }

    void ShowUIAtPoint(Vector3 position, Vector3 normal, BIMData data)
    {
        uiCanvas.SetActive(true);

        // Đẩy UI ra không khí 15cm
        uiCanvas.transform.position = position + normal * 0.15f;

        // Xoay UI về hướng Camera
        if (Camera.main != null)
        {
            uiCanvas.transform.LookAt(uiCanvas.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }

        string content = $"<b><color=yellow>{data.name}</color></b>\n";
        content += $"<size=80%>Type: {data.type}\nStorey: {data.storey}</size>\n\n";

        if (data.properties != null)
        {
            foreach (var p in data.properties)
            {
                // TÍNH NĂNG 2: BỎ QUA NẾU GIÁ TRỊ BỊ TRỐNG (NULL)
                if (p.Value == null) continue;

                string displayValue = p.Value.ToString();

                // TÍNH NĂNG 3: LÀM TRÒN SỐ THẬP PHÂN XUỐNG 2 CHỮ SỐ
                if (double.TryParse(displayValue, out double numericValue))
                {
                    displayValue = System.Math.Round(numericValue, 2).ToString();
                }

                content += $"<size=70%><color=#aaffaa>{p.Key}</color>: {displayValue}</size>\n";
            }
        }
        infoText.text = content;
    }
}