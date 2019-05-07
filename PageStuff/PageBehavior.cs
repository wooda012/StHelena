using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PageBehavior : MonoBehaviour
{
    int[] pageNums = { 0, 1, 2, 3 };
    Image UI_Page_1;
    Image UI_Page_2;
    Image UI_Page_3;
    Image UI_Page_4;
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject page4;
    public GameObject player;
    public GameObject UIPAGE1;
    public GameObject UIPAGE2;
    public GameObject UIPAGE3;
    public GameObject UIPAGE4;
    public Text TEXTPAGE1;
    public Text TEXTPAGE2;
    public Text TEXTPAGE3;
    public Text TEXTPAGE4;
    float page1dist;
    float page2dist;
    float page3dist;
    float page4dist;

    // Start is called before the first frame update
    void Start()
    {
        UI_Page_1 = UIPAGE1.GetComponent<Image>();
        UI_Page_2 = UIPAGE2.GetComponent<Image>();
        UI_Page_3 = UIPAGE3.GetComponent<Image>();
        UI_Page_4 = UIPAGE4.GetComponent<Image>();
        pageNums[0] = UnityEngine.Random.Range(1, 9);
        pageNums[1] = UnityEngine.Random.Range(1, 9);
        pageNums[2] = UnityEngine.Random.Range(1, 9);
        pageNums[3] = UnityEngine.Random.Range(1, 9);
    }

    // Update is called once per frame
    void Update()
    {
        //this if check for the mouse left click
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(page1 != null){
                page1dist = Vector3.Distance(player.transform.position, page1.transform.position);
            }
            if(page2 != null){
                page2dist = Vector3.Distance(player.transform.position, page2.transform.position);
            }
            if(page3 != null){
                page3dist = Vector3.Distance(player.transform.position, page3.transform.position);
            }
            if(page4 != null){
                page4dist = Vector3.Distance(player.transform.position, page4.transform.position);
            }
            if(page1 != null & page1dist < 4){
                Destroy(page1);
                UI_Page_1.color = UnityEngine.Color.white;
                TEXTPAGE1.text= getRiddle(pageNums[0]);
            }
            if(page2 != null & page2dist < 4){
                Destroy(page2);
                UI_Page_2.color = UnityEngine.Color.white;
                TEXTPAGE2.text= getRiddle(pageNums[1]);
            }
            if(page3 != null & page3dist < 4){
                Destroy(page3);
                UI_Page_3.color = UnityEngine.Color.white;
                TEXTPAGE3.text = getRiddle(pageNums[2]);
            }
            if(page4 != null & page4dist < 4){
                Destroy(page4);
                UI_Page_4.color = UnityEngine.Color.white;
                TEXTPAGE4.text = getRiddle(pageNums[3]);
            }
            /*
            //this if checks, a detection of hit in an GameObject with the mouse on screen
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10f))
                Debug.DrawRay(ray.origin, hit.point);

            //The name of the GameObject that you clicked will be stored in "hit.collider.gameObject.name"
            //Using that I compare that name with the page image name 
            //There will be 4 separate page objects each named page 1 through 4
                GameObject temppage = hit.collider.gameObject;

            //if we hit page1 then the UI_Page_1 is updated and the object in the game is destroyed
            if (temppage.name == "page1")
            {
                Destroy(temppage);
                UI_Page_1.color = UnityEngine.Color.white;
                //you can update the text of the image here too probably
            }

            //if we hit page2 then the UI_Page_2 is updated and the object in the game is destroyed
            if (hit.collider.gameObject.name == "page2")
            {
                Destroy(GameObject.Find(hit.collider.gameObject.name));
                UI_Page_2.color = UnityEngine.Color.white;
                //you can update the text of the image here too probably
            }

            //if we hit page3 then the UI_Page_3 is updated and the object in the game is destroyed
            if (hit.collider.gameObject.name == "page3")
            {
                Destroy(GameObject.Find(hit.collider.gameObject.name));
                UI_Page_3.color = UnityEngine.Color.white;
                //you can update the text of the image here too probably
            }

            //if we hit page4 then the UI_Page_4 is updated and the object in the game is destroyed
            if (hit.collider.gameObject.name == "page4")
            {
                Destroy(GameObject.Find(hit.collider.gameObject.name));
                UI_Page_4.color = UnityEngine.Color.white;
                //you can update the text of the image here too probably
            }
            */
        }//END if mouse is clicked statement
    }//END update
    public int[] getPageNums(){
        return pageNums;
    }

    string getRiddle(int pageNum){
        string text = "ERROR";
        switch (pageNum)
      {
        case 0:
              text = "nO one was there for me";
              break;
        case 1:
              text = "ONE day i was locked in my grave";  
              break;  
        case 2:
              text = "TWO kids got away";
              break;
        case 3:
              text = "THREE of them paid the price";
              break;
        case 4:
              text = "Nobody tried looking FOR me";
              break;
        case 5:
              text = "It was FIVE of them who would torment me";  
              break;  
        case 6:
              text = "I banged and screamed for someone to let me out for SIX days";
              break;
        case 7:
              text = "I gave in on the SEVENTH day";
              break;
        case 8:
              text = "I only rested for EIGHT weeks before they returned";
              break;
        case 9:
              text = "I was only NINE years old";     
              break;
      }
      return text;
    }
}//END class PageBehavior

    

