using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
