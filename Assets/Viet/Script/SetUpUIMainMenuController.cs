using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SetUpUIMainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] InputActionReference holdAction; 
    public XRRayInteractor rayInteractor;

    void Start()
    {
        if (mainMenu != null)
        {
            mainMenu.SetActive(false);
        }
    }

    void Update()
    {
        if (holdAction != null && holdAction.action != null && rayInteractor != null)
        {
            
            if (holdAction.action.IsPressed() && !rayInteractor.isActiveAndEnabled)
            {
                mainMenu.SetActive(true);
            }
            else
            {
                mainMenu.SetActive(false);
            }
        }
    }
}