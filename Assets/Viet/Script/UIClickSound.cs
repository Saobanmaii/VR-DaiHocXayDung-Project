using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIClickSound : MonoBehaviour
{
    void Start()
    {
        
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    private void PlayClickSound()
    {
        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound2D(SoundType.UI_Select);
        }
    }
}