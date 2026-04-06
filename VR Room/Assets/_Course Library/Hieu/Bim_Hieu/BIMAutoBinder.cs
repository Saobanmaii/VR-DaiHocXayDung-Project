using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;

public class BIMAutoBinder : EditorWindow
{
    private string mappingFilePath = "Assets/_Course Library/Hieu/Bim_Hieu/id_to_guid.json";

    private Dictionary<string, string> idToGuid;

    [MenuItem("Tools/BIM/Auto Bind Scene")]
    public static void ShowWindow()
    {
        GetWindow<BIMAutoBinder>("BIM Auto Binder");
    }

    void OnGUI()
    {
        GUILayout.Label("Mapping File (id_to_guid.json)", EditorStyles.boldLabel);

        mappingFilePath = EditorGUILayout.TextField("Path", mappingFilePath);

        if (GUILayout.Button("Load Mapping"))
        {
            LoadMapping();
        }

        if (GUILayout.Button("Bind Entire Scene"))
        {
            BindAll();
        }

        if (GUILayout.Button("Bind Selected Root"))
        {
            if (Selection.activeGameObject != null)
            {
                BindRecursive(Selection.activeGameObject);
            }
        }
    }

    // ===== LOAD JSON =====

    void LoadMapping()
    {
        if (!File.Exists(mappingFilePath))
        {
            Debug.LogError("Mapping file not found!");
            return;
        }

        string json = File.ReadAllText(mappingFilePath);
        idToGuid = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        Debug.Log($"Loaded mapping: {idToGuid.Count} entries");
    }

    // ===== BIND ALL =====

    [System.Obsolete]
    void BindAll()
    {
        if (idToGuid == null)
        {
            Debug.LogError("Load mapping first!");
            return;
        }

        int count = 0;

        foreach (GameObject go in FindObjectsOfType<GameObject>())
        {
            if (Bind(go))
                count++;
        }

        Debug.Log($"Bound {count} objects.");
    }

    void BindRecursive(GameObject root)
    {
        int count = 0;

        foreach (Transform t in root.GetComponentsInChildren<Transform>(true))
        {
            if (Bind(t.gameObject))
                count++;
        }

        Debug.Log($"Bound {count} objects under {root.name}");
    }

    // ===== CORE LOGIC =====

    bool Bind(GameObject go)
    {
        string id = ExtractId(go.name);
        if (id == null) return false;

        if (!idToGuid.TryGetValue(id, out string guid))
            return false;

        var comp = go.GetComponent<BIMElement>();
        if (comp == null)
            comp = go.AddComponent<BIMElement>();

        comp.GUID = guid;

        return true;
    }

    string ExtractId(string name)
    {
        var match = Regex.Match(name, @"\[(\d+)\]");
        return match.Success ? match.Groups[1].Value : null;
    }
}