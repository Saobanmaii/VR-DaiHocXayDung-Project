using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor; 
#endif

public class GlobalButtonSound : MonoBehaviour
{

    [ContextMenu("Setup All Button Sounds")]
    public void SetupAllButtons()
    {
        #if UNITY_EDITOR
      
        Button[] allButtons = Object.FindObjectsOfType<Button>(true);
        int addedCount = 0;

        foreach (Button btn in allButtons)
        {
           
            if (btn.gameObject.GetComponent<UIClickSound>() == null)
            {
                btn.gameObject.AddComponent<UIClickSound>();
                
              
                EditorUtility.SetDirty(btn.gameObject);
                addedCount++;
            }
        }

        Debug.Log($"<color=green><b>[Editor Tool]</b></color> Đã gắn xong script âm thanh cho {addedCount} nút mới!");
        
       
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        #endif
    }
}