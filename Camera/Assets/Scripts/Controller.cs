using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float speed;
    public float jumpheight;
    public LayerMask ground;
    public Transform feet;
    public Transform shoulder;
    public Light flashLight;
    public float rotationSpeed;

    private Vector3 direction;
    private float rotationY;
    private float rotationX;
    private Rigidbody rbody;
    private float minY;
    private float maxY;
    private bool fLight = true;


    // Start is called before the first frame update
    void Start()
    {
        jumpheight = 3.0f;
        rotationX = 0;
        rotationY = 0;
        rotationSpeed = 3.0f;
        speed = 3f;
        minY = -60f;
        maxY = 60f;
        rbody = GetComponent<Rigidbody>();
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = direction.normalized;
        if ((int)direction.x != 0)
        {
            rbody.MovePosition(rbody.position + transform.right * direction.x * speed * Time.deltaTime);
        }
        if ((int)direction.z != 0)
        {
            rbody.MovePosition(rbody.position + transform.forward * direction.z * speed * Time.deltaTime);
        }
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
        rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        bool isGrounded = Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rbody.AddForce(Vector3.up * Mathf.Sqrt(jumpheight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (fLight) { shoulder.Rotate(45f, 0f, 0f, Space.Self); }
            else { shoulder.Rotate(-45f, 0f, 0f, Space.Self); }
            fLight = !fLight;
            flashLight.enabled = fLight;
        }

    }
}