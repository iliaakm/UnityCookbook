using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    List<Menu> menus = new List<Menu>();

    void Start()
    {
        ShowMenu(menus[0]);
    }

    public void ShowMenu(Menu menuToShow)
    {
        if(!menus.Contains(menuToShow))
        {
            Debug.LogError($"{menuToShow.name} is not in the list of menus");
            return;
        }

        foreach (var otherMenu in menus)
        {
            if (otherMenu == menuToShow)
            {
                otherMenu.gameObject.SetActive(true);
                otherMenu.menuDidAppear.Invoke();
            }
            else
            {
                if(otherMenu.gameObject.activeInHierarchy)
                {
                    otherMenu.menuWillDisappear.Invoke();
                }
                otherMenu.gameObject.SetActive(false);
            }
        }
    } 
}
