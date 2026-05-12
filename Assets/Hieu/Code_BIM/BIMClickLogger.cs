using UnityEngine;
using UnityEngine.InputSystem;

public class BIMClickLogger : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit))
            {
                GameObject obj = hit.collider.gameObject;

                var bim = obj.GetComponent<BIMElement>();
                if (bim == null)
                {
                    Debug.Log("No BIM data on object: " + obj.name);
                    return;
                }

                var data = BIMDatabase.Get(bim.GUID);
                if (data == null)
                {
                    Debug.Log("No data found for GUID: " + bim.GUID);
                    return;
                }

                LogData(data);
            }
        }
    }

    void LogData(BIMData data)
    {
        Debug.Log("===== BIM DATA =====");
        Debug.Log($"GUID: {data.guid}");
        Debug.Log($"Name: {data.name}");
        Debug.Log($"Type: {data.type}");
        Debug.Log($"Storey: {data.storey}");
        Debug.Log($"ElementId: {data.elementId}");

        if (data.properties != null)
        {
            foreach (var kv in data.properties)
            {
                Debug.Log($"{kv.Key}: {kv.Value}");
            }
        }
    }
}
