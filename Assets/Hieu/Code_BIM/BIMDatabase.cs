using UnityEngine;
using System.Collections.Generic;
// Đã xóa dòng using System.IO; vì không cần mò mẫm đường dẫn ổ cứng nữa
using Newtonsoft.Json;

public class BIMDatabase : MonoBehaviour
{
    public static Dictionary<string, BIMData> Data;

    [Header("Kéo file mep_data.json vào đây")]
    public TextAsset jsonFile; // <-- CÚ LỘT XÁC Ở ĐÂY

    void Awake()
    {
        Load();
    }

    void Load()
    {
        // Kiểm tra xem a đã kéo thả file vào chưa
        if (jsonFile == null)
        {
            Debug.LogError("LỖI CHÍ MẠNG: A chưa kéo file JSON vào cục BIMDatabase ở ngoài Inspector!");
            return;
        }

        // Rút ruột nội dung JSON từ cục TextAsset (bao sống trên mọi nền tảng kể cả kính VR)
        string json = jsonFile.text;
        Data = JsonConvert.DeserializeObject<Dictionary<string, BIMData>>(json);

        Debug.Log("Loaded BIM data thành công: " + Data.Count + " cấu kiện!");
    }

    public static BIMData Get(string guid)
    {
        if (Data == null) return null;
        return Data.TryGetValue(guid, out var d) ? d : null;
    }
}