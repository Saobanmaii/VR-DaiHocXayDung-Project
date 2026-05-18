
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum RoleButton
{
    Map,
    Setting,
    MainMenu
}

public class AnimationControllerButton : MonoBehaviour
{
    [SerializeField] List<AnimationControllerButton> buttonOrther;
    [SerializeField]  List<GameObject>maps; // 1 map, 2 setting, 3 main menu
    [SerializeField] List<String> stringsMaps; // 1 map, 2 setting, 3 main menu
    // public TextMeshProUGUI textMap;
    public RoleButton roleButton;
    public Color colorSelect;
    public Color colorDefault;
    
    Animator animator;
    public bool selected = false;
    Image image;
    void Start()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
        image.color = colorDefault;
    }

    public void select()
    {
        if (!selected)
        {
            SelectFunction();
            foreach (var item in buttonOrther)
            {
                item.deselect();
            }
            animator.SetBool("Select", true);
            animator.SetBool("Hover", true);
            selected = true;
            image.color = colorSelect;
            Debug.Log("Select");
        }
        else
        {
            deselect();
            unselectFunction();
        }
        
    }

    private void SelectFunction()
    {
        switch (roleButton)
        {
            case RoleButton.Map:
                maps[0].SetActive(true);
                maps[1].SetActive(false);
                maps[2].SetActive(false);
                break;
            case RoleButton.Setting:
                maps[0].SetActive(false);
                maps[1].SetActive(true);
                maps[2].SetActive(false);
                break;
            case RoleButton.MainMenu:
                maps[0].SetActive(false);
                maps[1].SetActive(false);
                maps[2].SetActive(true);
                break;
        }
    }

    private void unselectFunction()
    {
        switch (roleButton)
        {
            case RoleButton.Map:
                maps[0].SetActive(false);
                break;
            case RoleButton.Setting:
                maps[1].SetActive(false);
                break;
            case RoleButton.MainMenu:
                maps[2].SetActive(false);
                break;
        }
    }

    public void deselect()
    {
        animator.SetBool("Select", false);
        animator.SetBool("Hover", false);
        image.color = colorDefault;
        Debug.Log("Deselect");
        selected = false;
    }

    public void hover()
    {
        animator.SetBool("Hover", true);
        AdjustTextMainMenu();

    }

    private void AdjustTextMainMenu()
    {
        // if(textMap == null) return;
        // switch (roleButton)
        // {
        //     case RoleButton.Map:
        //         textMap.text = stringsMaps[0];
        //         break;
        //     case RoleButton.Setting:
        //         textMap.text = stringsMaps[1];
        //         break;
        //     case RoleButton.MainMenu:
        //         textMap.text = stringsMaps[2];
        //         break;
        // }
    }

    public void unhover()
    {
        animator.SetBool("Hover", false);
    }
}
