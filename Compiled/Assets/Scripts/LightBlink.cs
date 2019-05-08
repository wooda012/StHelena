using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    private Random random = new Random();
    [SerializeField] private Light spot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Random.Range(0f, 1000f) <= 50)
        {
            spot.enabled = !spot.enabled;
        }
    }
}
