using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{

	[SerializeField] private GameObject unlockUI;
	[SerializeField] private bool isOpen;
	public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject page4;
    private string finalCode;
    public GameObject player;
    public InputField num1;
    public InputField num2;
    public InputField num3;
    public InputField num4;
    public AudioSource badUnlock;
    public GameObject pageSource;

    // Start is called before the first frame update
    void Start()
    {
    	int[] numbers = pageSource.GetComponent<PageBehavior>().getPageNums();
        finalCode = numbers[0].ToString() + numbers[1].ToString() + numbers[2].ToString() + numbers[3].ToString();
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
    	float dist = Vector3.Distance(player.transform.position, gameObject.transform.position);
    	if( dist < 4 ){
    		if(Input.GetKeyDown(KeyCode.F)){
    			if(!isOpen){
    				isOpen = true;
    				unlockUI.SetActive(true);
    				player.GetComponent<Controller>().enabled = false;
    				Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
    			}
    			else{
    				isOpen = false;
    				unlockUI.SetActive(false);
    				player.GetComponent<Controller>().enabled = true;
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
    			}
    		}
    	}	
        
    }
    public void unlock(){
    	string number1 = num1.text;
    	string number2 = num2.text;
    	string number3 = num3.text;
    	string number4 = num4.text;
    	string inputCode = number1 + number2 + number3 + number4;
    	if(inputCode == finalCode){
    		int i = Application.loadedLevel;
			Application.LoadLevel(i+1);
    	}
    	else{
    		badUnlock.Play();
    	}
    }
}
