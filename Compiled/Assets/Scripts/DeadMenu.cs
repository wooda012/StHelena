using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RetryButtClick()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuButtClick()
    {
        SceneManager.LoadScene(0);
    }

    public void Leave()
    {
        Application.Quit(0);
    }

}
