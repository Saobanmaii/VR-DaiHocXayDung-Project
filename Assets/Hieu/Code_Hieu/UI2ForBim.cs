using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class UI2ForBim : MonoBehaviour
{
    [Header("Cấu hình XR Ray")]
    public XRRayInteractor xrRay;

    [Header("Cấu hình UI MỚI")]
    public GameObject uiCanvas;

    // Khai báo từng ô chữ trong bảng thiết kế của a
    public TextMeshProUGUI txt_ChieuDai;
    public TextMeshProUGUI txt_ChieuRong;
    public TextMeshProUGUI txt_ChieuCao;
    public TextMeshProUGUI txt_KhoiLuong;
    public TextMeshProUGUI txt_VatLieu;

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
        uiCanvas.transform.position = position + normal * 0.15f;

        if (Camera.main != null)
        {
            uiCanvas.transform.LookAt(uiCanvas.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }

        // ==========================================
        // KHÚC MỚI: TÌM VÀ ĐIỀN ĐÚNG Ô (MAPPING)
        // ==========================================

        // 1. Xóa sạch data cũ mỗi lần bật bảng lên (phòng hờ vật không có thông số đó)
        txt_ChieuDai.text = "---";
        txt_ChieuRong.text = "---";
        txt_ChieuCao.text = "---";
        txt_KhoiLuong.text = "---";
        txt_VatLieu.text = "---";

        if (data.properties != null)
        {
            // 2. Đi dò từng thông số trong file JSON xem có khớp tên không
            // Lưu ý: Chữ trong ngoặc kép "..." phải gõ GIỐNG HỆT tên biến trong file JSON của a nhé

            if (data.properties.ContainsKey("Length"))
                txt_ChieuDai.text = FormatNumber(data.properties["Length"].ToString());

            if (data.properties.ContainsKey("Width"))
                txt_ChieuRong.text = FormatNumber(data.properties["Width"].ToString());

            if (data.properties.ContainsKey("Height"))
                txt_ChieuCao.text = FormatNumber(data.properties["Height"].ToString());

            if (data.properties.ContainsKey("Mass") || data.properties.ContainsKey("Weight"))
                txt_KhoiLuong.text = FormatNumber(data.properties["Mass"].ToString()); // Nhớ sửa "Mass" thành chữ thực tế trong JSON

            if (data.properties.ContainsKey("Material"))
                txt_VatLieu.text = data.properties["Material"].ToString();
        }
    }

    // Hàm phụ trợ để tự động làm tròn số cho sạch đẹp, không bị lỗi
    private string FormatNumber(string rawValue)
    {
        if (rawValue == null) return "---";

        if (double.TryParse(rawValue, out double numericValue))
        {
            return System.Math.Round(numericValue, 2).ToString();
        }
        return rawValue; // Nếu là chữ (không ép sang số được) thì giữ nguyên
    }
}