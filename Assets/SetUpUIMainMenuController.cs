using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class SetUpUIMainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    
   
    [SerializeField] InputActionReference holdAction; 

    void Start()
    {
        if (mainMenu != null)
        {
            mainMenu.SetActive(false);
        }
    }

   
    void Update()
    {
        
        if (holdAction != null && holdAction.action != null)
        {
            
            if (holdAction.action.IsPressed())
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