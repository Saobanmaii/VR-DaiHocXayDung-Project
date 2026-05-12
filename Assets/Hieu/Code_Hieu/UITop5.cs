using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class UITop5 : MonoBehaviour
{
    [Header("Cấu hình XR Ray")]
    public XRRayInteractor xrRay;

    [Header("Cấu hình UI")]
    public GameObject uiCanvas;

    [Header("Ô Tên Cấu Kiện (Trên Cùng)")]
    public TextMeshProUGUI txt_Ten;

    [Header("HÀNG 1")]
    public TextMeshProUGUI lbl_1; // Ô chứa Tên Mục gốc (Key)
    public TextMeshProUGUI val_1; // Ô chứa Giá trị (Value)

    [Header("HÀNG 2")]
    public TextMeshProUGUI lbl_2;
    public TextMeshProUGUI val_2;

    [Header("HÀNG 3")]
    public TextMeshProUGUI lbl_3;
    public TextMeshProUGUI val_3;

    [Header("HÀNG 4")]
    public TextMeshProUGUI lbl_4;
    public TextMeshProUGUI val_4;

    [Header("HÀNG 5")]
    public TextMeshProUGUI lbl_5;
    public TextMeshProUGUI val_5;

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

        // 1. Reset trắng toàn bộ 10 ô trước khi điền data mới
        txt_Ten.text = "---";
        lbl_1.text = ""; val_1.text = "";
        lbl_2.text = ""; val_2.text = "";
        lbl_3.text = ""; val_3.text = "";
        lbl_4.text = ""; val_4.text = "";
        lbl_5.text = ""; val_5.text = "";

        // 2. Điền tên cấu kiện
        if (!string.IsNullOrEmpty(data.name))
        {
            txt_Ten.text = data.name;
        }

        // 3. Tự động múc 5 thông số đầu tiên đưa lên UI
        if (data.properties != null)
        {
            int rowCount = 1;
            foreach (var prop in data.properties)
            {
                if (rowCount > 5) break; // Chỉ lấy đúng 5 cái đầu tiên, thừa thì bỏ

                // Lấy thẳng Key (tiếng Anh) làm Tên Mục, Value làm Giá Trị
                string tenMucGoc = prop.Key;

                // Tránh lỗi null nếu giá trị bị trống
                string giaTri = (prop.Value != null) ? FormatNumber(prop.Value.ToString()) : "---";

                // Đẩy vào đúng hàng tương ứng
                if (rowCount == 1) { lbl_1.text = tenMucGoc; val_1.text = giaTri; }
                else if (rowCount == 2) { lbl_2.text = tenMucGoc; val_2.text = giaTri; }
                else if (rowCount == 3) { lbl_3.text = tenMucGoc; val_3.text = giaTri; }
                else if (rowCount == 4) { lbl_4.text = tenMucGoc; val_4.text = giaTri; }
                else if (rowCount == 5) { lbl_5.text = tenMucGoc; val_5.text = giaTri; }

                rowCount++;
            }
        }
    }

    // Hàm tự động làm tròn số (giữ nguyên)
    private string FormatNumber(string rawValue)
    {
        if (string.IsNullOrEmpty(rawValue)) return "---";

        if (double.TryParse(rawValue, out double numericValue))
        {
            return System.Math.Round(numericValue, 2).ToString();
        }
        return rawValue;
    }
}