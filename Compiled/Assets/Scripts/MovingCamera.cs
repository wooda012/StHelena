using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovingCamera : MonoBehaviour
{

    public Camera cam;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject credits;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.Rotate(Vector2.up, -3.0f * Time.deltaTime);
    }

    public void StartButtClick()
    {
        SceneManager.LoadScene(1);
    }

    public void NavButtClick()
    {
        startMenu.SetActive(!startMenu.active);
        credits.SetActive(!credits.active);
    }

    public void Leave()
    {
        Application.Quit(0);
    }

}
