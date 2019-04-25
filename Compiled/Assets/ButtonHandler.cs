using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    public Button button;
    public Color newColor;

    //Changes the button's Normal color to the new color.
    ColorBlock cb; 

    void Start()
    {
        cb = button.colors;
        cb.normalColor = newColor;
        button.colors = cb;

    }

    private void Update()
    {
        cb.normalColor = newColor;
    }

    public void MouseEnter()
    {
        newColor = Color.grey;
    }

    public void MouseDown()
    {
        newColor = Color.white;
    }

    public void MouseExit()
    {
        newColor = Color.black;
    }
}
