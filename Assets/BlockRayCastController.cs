
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BlockRayCastController : MonoBehaviour
{
    public static TeleportationArea _teleport;
    public GameObject gameObjectTeleportArea;

    void Awake()
    {
        _teleport=gameObjectTeleportArea.GetComponent<TeleportationArea>();
    }
    void Start()
    {
        setBlockUnBlock(false);
    }
    // Update is called once per frame
    public static void setBlockUnBlock(bool _bool)
    {
        _teleport.enabled=_bool;
    }
}
