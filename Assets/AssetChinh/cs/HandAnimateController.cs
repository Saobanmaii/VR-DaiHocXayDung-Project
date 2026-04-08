using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimateController : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionProperty triggerAction; 
    public InputActionProperty gripAction;    

    [Header("Animator")]
    public Animator handAnimator;

    [Range(0, 1)]
    public float threshold = 0.5f; 

void Update()
    {
        bool isTriggerPressed = triggerAction.action.IsPressed();
        bool isGripPressed = gripAction.action.IsPressed();

        handAnimator.SetBool("TriggerPressed", isTriggerPressed);
        handAnimator.SetBool("GripPressed", isGripPressed);
    }
}