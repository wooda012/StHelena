using System;
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
    public float stamina;
    public LayerMask ground;
    [SerializeField]  private Transform feet;
    [SerializeField]  private Transform shoulder;
    [SerializeField] private Transform waist;
    [SerializeField]  private Transform head;
    [SerializeField] private GameObject phone;
    [SerializeField]  private Light flashLight;
    [SerializeField]  private float rotationSpeed;
    [SerializeField]  private Image firstPower;
    [SerializeField]  private Image secondPower;
    [SerializeField]  private Image lastPower;
    [SerializeField] private Image fullCharge1;
    [SerializeField] private Image fullCharge2;
    [SerializeField] private Image fullCharge3;
    [SerializeField] private Image fullCharge4;
    [SerializeField] private Image staminaBar;
    [SerializeField] private Image emptyStamina;




    private Vector3 direction;
    private float originalSpeed;
    private float originalStamina;
    private float maxDuration;
    private float rotationY;
    private float rotationX;
    private Rigidbody rbody;
    private int chargeCount = 0;
    private float minY;
    private float maxY;
    private bool fLight = true;
    public Color phoneBars;
    private int TurnCountLeft = 0;
    private int TurnCountRight = 0;
    private string playState;



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
        chargeCount = 0;
        originalStamina = stamina;
    }

    // Update is called once per frame
    void Update()
    {
        //Controls the camera and movement
        direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        if (direction.x != 0)
        {
            rbody.MovePosition(rbody.position + transform.right * direction.x * speed * Time.deltaTime);
        }
        if(direction.z != 0)
        {
           rbody.MovePosition(rbody.position + transform.forward * direction.z * speed * Time.deltaTime);
        }
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
        rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore))
            {
                rbody.AddForce(Vector3.up * Mathf.Sqrt(jumpheight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }
        }

        Sprint();

        //Turn the flashlight on and off
        if (phone.active)
        {
            TurnOutletsBack();
            SpendLight();
        }
        else
        {
            ChargeLight();
        }

        TurnHead();



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
        if(speed <= originalSpeed)
        {
            if(Input.GetKey("q") && TurnCountLeft < 6)
            {
                waist.Rotate(0f, 0f, 200f * Time.deltaTime, Space.Self);
                waist.Translate(-3f*Time.deltaTime, 3f * Time.deltaTime, 0f, Space.Self);
                TurnCountLeft++;
                playState = "Leaning";
            }
            if(Input.GetKey("e") && TurnCountRight < 6)
            {
                waist.Rotate(0f, 0f, -200f * Time.deltaTime, Space.Self);
                waist.Translate(3f * Time.deltaTime, 3f * Time.deltaTime, 0f, Space.Self);
                TurnCountRight++;
                playState = "Leaning";
            }
            if(!Input.GetKey("q") && TurnCountLeft > 0)
            {
                waist.Rotate(0f, 0f, -200f * Time.deltaTime, Space.Self);
                waist.Translate(3f * Time.deltaTime, -2.5f * Time.deltaTime, 0f, Space.Self);
                TurnCountLeft--;
            }
            if (!Input.GetKey("e") && TurnCountRight > 0)
            {
                waist.Rotate(0f, 0f, 200f * Time.deltaTime, Space.Self);
                waist.Translate(-3f * Time.deltaTime, -2.5f * Time.deltaTime, 0f, Space.Self);
                TurnCountRight--;

            }
            if (TurnCountLeft == 0 && TurnCountRight == 0)
            {
                waist.transform.SetPositionAndRotation(waist.position, new Quaternion(0f, 0f, 0f, 0f));
                playState = "Standing";
            }

        }
        else if(speed > originalSpeed)
        {
            if (Input.GetKey("q"))
            {
                if (TurnCountLeft < 6)
                {
                    head.Rotate(0f, -850f * Time.deltaTime, 0f, Space.Self);
                    TurnCountLeft++;
                }
            }
            if (Input.GetKey("e"))
            {
                if (TurnCountRight < 6)
                {
                    head.Rotate(0f, 850f * Time.deltaTime, 0f, Space.Self);
                    TurnCountRight++;
                }
            }
            if (!(Input.GetKey("q")) || ((stamina < 0 || speed == originalSpeed) && playState == "Standing"))
            {
                if (TurnCountLeft > 0)
                {
                    head.Rotate(0f, 850f * Time.deltaTime, 0f, Space.Self);
                    TurnCountLeft--;
                }
            }
            if (!Input.GetKey("e") || ((stamina == 0 || speed == originalSpeed) && playState == "Standing"))
            {
                if (TurnCountRight > 0) {
                    head.Rotate(0f, -850f * Time.deltaTime, 0f, Space.Self);
                    TurnCountRight--;
                }
            }
            if(TurnCountLeft == 0 && TurnCountRight == 0)
            {
                head.transform.SetPositionAndRotation(head.position, new Quaternion(0f, 0f, 0f, 0f));
            }
        }
    }

    private void Sprint() 
    {
        if (playState == "Standing" || playState == "Sprinting")
        {
            //Sprinting is controlled here
            if (Input.GetKeyDown(KeyCode.LeftShift) && speed.CompareTo(originalSpeed) == 0)
            {
                speed += 2;
                playState = "Sprinting";
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && speed > originalSpeed)
            {
                speed -= 2;
                playState = "Standing";
            }
        }

        if (speed > originalSpeed && (direction.x!= 0 || direction.z != 0))
        {
            stamina--;
        }
        else if (stamina < originalStamina)
        {
            stamina += 2;
        }

        if (stamina == 0 && speed > originalSpeed)
        {
            speed -= 3;
            stamina -= 60;
            staminaBar.enabled = false;
            emptyStamina.enabled = true;
            playState = "Standing";
        }
        else if (stamina == originalStamina)
        {
            speed = originalSpeed;
            emptyStamina.enabled = false;
            staminaBar.enabled = true;
        }
        if(stamina >= 0) {
            staminaBar.fillAmount = stamina / originalStamina;
            emptyStamina.fillAmount = stamina / originalStamina;
        }
    }

    private void ChargeLight()
    {
        flashLight.enabled = fLight;
        lastPower.enabled = true;
        if (chargeCount != 0 && flashDuration < maxDuration)
        {
            flashDuration++;
        }
        if (flashDuration.CompareTo((maxDuration / 3) * 2) == 0)
        {
            firstPower.enabled = true;
        }
        else if (flashDuration.CompareTo((maxDuration / 3)) == 0)
        {
            secondPower.enabled = true;
            lastPower.color = phoneBars;
        }
        else if(flashDuration == maxDuration)
        {
            TurnOutlets();
        }
        chargeCount = (chargeCount + 1) % 3;
    }

    private void TurnOutlets()
    {
       fullCharge1.color = phoneBars;
       fullCharge2.color = phoneBars; 
       fullCharge3.color = phoneBars;
       fullCharge4.color = phoneBars;

    }

    private void TurnOutletsBack()
    {
        fullCharge1.color = Color.white;
        fullCharge2.color = Color.white;
        fullCharge3.color = Color.white;
        fullCharge4.color = Color.white;

    }

    private void SpendLight()
    {
        if (Input.GetKeyDown("z"))
        {
            TurnLight();
        }

        //Keeps track of flashlight charge
        if (fLight && flashDuration.CompareTo(0) != 0)
        {
            flashDuration--;
            if (flashDuration.CompareTo((maxDuration / 3) * 2) == 0)
            {
                firstPower.enabled = false;
            }
            else if (flashDuration.CompareTo((maxDuration / 3)) == 0)
            {
                secondPower.enabled = false;
                lastPower.color = Color.red;
            }
        }
        else if (flashDuration.CompareTo(0) == 0)
        {
            lastPower.enabled = false;
            flashLight.enabled = false;
        }
    }
}