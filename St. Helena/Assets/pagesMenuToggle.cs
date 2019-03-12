using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pagesMenuToggle : MonoBehaviour
{
    [SerializeField] private GameObject pagesMenuUI;
    [SerializeField] private bool isOpen;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            isOpen = !isOpen;
        }
        if(isOpen)
        {
            ActivateMenu();
        }
        else if(!isOpen)
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
          pagesMenuUI.SetActive(true);
    }

    void DeactivateMenu()
    {
        pagesMenuUI.SetActive(false);
    }

}