using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float speed;
    public float jumpheight;
    public float flashDuration; //keeps track of how long the flashlight lasts, in minutes
    public LayerMask ground;
    [SerializeField]  private Transform feet;
    [SerializeField]  private Transform shoulder;
    [SerializeField]  private Transform waist;
    [SerializeField]  private Transform head;
    [SerializeField]  private Light flashLight;
    [SerializeField]  private float rotationSpeed;
    [SerializeField]  private Image firstPower;
    [SerializeField]  private Image secondPower;
    [SerializeField]  private Image lastPower;

    private Vector3 direction;
    private float originalSpeed;
    private float maxDuration;
    private float rotationY;
    private float rotationX;
    private Rigidbody rbody;
    private float minY;
    private float maxY;
    private bool fLight = true;


    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = speed;
        flashDuration *= 3600;
        maxDuration = flashDuration;
        rotationX = 0;
        rotationY = 0;
        minY = -60f;
        maxY = 60f;
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Controls the camera
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        if ((int)direction.x != 0)
        {
            rbody.MovePosition(rbody.position + transform.right * direction.x * speed * Time.deltaTime);
        }
        if((int)direction.z != 0)
        {
           rbody.MovePosition(rbody.position + transform.forward * direction.z * speed * Time.deltaTime);
        }
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
        rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        bool isGrounded = Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore);

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rbody.AddForce(Vector3.up * Mathf.Sqrt(jumpheight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        //Keeps track of flashlight charge
        if(fLight && flashDuration.CompareTo(0) != 0)
        {
            flashDuration--;
            if(flashDuration.CompareTo((maxDuration/3)*2) == 0)
            {
                firstPower.enabled = false;
            }
            else if(flashDuration.CompareTo((maxDuration / 3)) == 0)
            {
                secondPower.enabled = false;
                lastPower.color = Color.red;
            }
        }
        else if(flashDuration.CompareTo(0) == 0)
        {
            lastPower.enabled = false;
            flashLight.enabled = false;
        }

        //Turn the flashlight on and off
        if (Input.GetKeyDown("z"))
        {
            TurnLight();
        }

        //Controls leaning the camera
        if(Input.GetKeyDown("q") || Input.GetKeyDown("e") || Input.GetKeyUp("q") || Input.GetKeyUp("e")) 
        {
            TurnHead();
        }

        //Sprinting is controlled here
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 2;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= 2;
        }

    }

    //Function to help turn flashlight on and off
    void TurnLight()
    {
        if (fLight) { shoulder.Rotate(45f, 0f, 0f, Space.Self); }
        else { shoulder.Rotate(-45f, 0f, 0f, Space.Self); }
        fLight = !fLight;
        if (flashDuration.CompareTo(0) != 0)
        {
            flashLight.enabled = fLight;
        }
    }

    //Function to help with turning and leaning
    void TurnHead()
    {
        if(speed.Equals(originalSpeed))
        {
            if(Input.GetKeyDown("q"))
            {
                waist.Rotate(0f, 0f, 30f, Space.Self);
            }
            if(Input.GetKeyDown("e"))
            {
                waist.Rotate(0f, 0f, -30f, Space.Self);
            }
            if(Input.GetKeyUp("q"))
            {
                waist.Rotate(0f, 0f, -30f, Space.Self);
            }
            if(Input.GetKeyUp("e"))
            {
                waist.Rotate(0f, 0f, 30f, Space.Self);
            }
        }
        else if(speed.CompareTo(originalSpeed) > 0)
        {
            if (Input.GetKeyDown("q"))
            {
                head.Rotate(0f, 120f, 0f, Space.Self);
            }
            if (Input.GetKeyDown("e"))
            {
                head.Rotate(0f, -120f, 0f, Space.Self);
            }
            if (Input.GetKeyUp("q"))
            {
                head.Rotate(0f, -120f, 0f, Space.Self);
            }
            if (Input.GetKeyUp("e"))
            {
                head.Rotate(0f, 120f, 0f, Space.Self);
            }
        }
    }
}