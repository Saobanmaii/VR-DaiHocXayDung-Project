using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit; // Thư viện XR của Unity

public class BIM_XR_UI_Bridge : MonoBehaviour
{
    [Header("XR Settings")]
    public XRRayInteractor xrRayInteractor; // Kéo tay cầm có tia vào đây

    [Header("UI Settings")]
    public GameObject uiCanvas;        // Kéo cái Canvas ở Bước 1 vào đây
    public TextMeshProUGUI infoText;   // Kéo cái BimInfoText vào đây

    void Update()
    {
        if (xrRayInteractor == null) return;

        RaycastHit hit;
        // Kiểm tra xem tia XR hiện tại có đang chạm trúng vật thể 3D nào không
        if (xrRayInteractor.TryGetCurrent3DRaycastHit(out hit))
        {
            // Tìm component BIMElement trên vật thể đó
            BIMElement bim = hit.collider.GetComponent<BIMElement>();

            if (bim != null)
            {
                UpdateUI(hit.point, hit.normal, bim.GUID);
            }
            else
            {
                uiCanvas.SetActive(false);
            }
        }
        else
        {
            uiCanvas.SetActive(false);
        }
    }

    void UpdateUI(Vector3 position, Vector3 normal, string guid)
    {
        // Gọi hàm Get của mentor để lấy data từ JSON
        BIMData data = BIMDatabase.Get(guid);

        if (data != null)
        {
            uiCanvas.SetActive(true);

            // Đưa bảng UI đến vị trí va chạm, nhích ra ngoài 5cm để không lún vào tường
            uiCanvas.transform.position = position + normal * 0.05f;

            // Xoay bảng UI hướng về phía mắt người chơi (Main Camera)
            uiCanvas.transform.LookAt(uiCanvas.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

            // Ghi nội dung (Giống logic LogData nhưng đổ vào Text)
            string content = $"<b><color=yellow>{data.name}</color></b>\n";
            content += $"<size=80%>Type: {data.type}\nStorey: {data.storey}</size>\n\n";

            if (data.properties != null)
            {
                foreach (var p in data.properties)
                    content += $"<size=70%><color=#aaffaa>{p.Key}</color>: {p.Value}</size>\n";
            }
            infoText.text = content;
        }
    }
}