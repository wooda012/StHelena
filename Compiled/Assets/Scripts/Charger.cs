using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [SerializeField] private GameObject outletPhone;
    [SerializeField] private GameObject playerPhone;
    public LayerMask player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChargePhone()
    {
        if(Physics.CheckSphere(outletPhone.transform.position, 3f, player, QueryTriggerInteraction.Ignore)) {
            outletPhone.SetActive(!outletPhone.active);
            playerPhone.SetActive(!playerPhone.active);
        }
    }
}
