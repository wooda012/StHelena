using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pagesMenuToggle : MonoBehaviour
{
    [SerializeField] private GameObject pagesMenuUI;
    [SerializeField] private GameObject chargeMessage;
    [SerializeField] private UnityEngine.Events.UnityEvent chargePhone;
    [SerializeField] private bool isOpen;

    private RaycastHit outlets;

    private void Update()
    {
        int layerMask = 1 << 10;
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

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out outlets, 2f, layerMask))
        {
            chargeMessage.SetActive(true);
            if(Input.GetKeyDown("f"))
            {
                chargePhone.Invoke();
            }
        }
        else chargeMessage.SetActive(false);
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