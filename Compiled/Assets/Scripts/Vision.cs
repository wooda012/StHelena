using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
         if (Physics.Raycast(transform.position, transform.forward, out hit)){
         	 if(hit.distance < 1 && Input.GetKeyDown("e")){
             	hit.transform.SendMessage("Pressing");
             }
             hit.transform.SendMessage ("LookingAt");
         }
    }
}
