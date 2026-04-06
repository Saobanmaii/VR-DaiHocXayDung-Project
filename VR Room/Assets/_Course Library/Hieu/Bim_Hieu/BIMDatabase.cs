using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class BIMDatabase : MonoBehaviour
{
    public static Dictionary<string, BIMData> Data;

    [Header("Path to mep_data.json")]
    public string jsonPath = "Assets/_Course Library/Hieu/Bim_Hieu/mep_data.json";

    void Awake()
    {
        Load();
    }

    void Load()
    {
        if (!File.Exists(jsonPath))
        {
            Debug.LogError("JSON not found: " + jsonPath);
            return;
        }

        string json = File.ReadAllText(jsonPath);
        Data = JsonConvert.DeserializeObject<Dictionary<string, BIMData>>(json);

        Debug.Log("Loaded BIM data: " + Data.Count);
    }

    public static BIMData Get(string guid)
    {
        if (Data == null) return null;
        return Data.TryGetValue(guid, out var d) ? d : null;
    }
}