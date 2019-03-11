using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPController : MonoBehaviour
{
	public float speed;


	private Vector3 direction;
	private Rigidbody rbody;

	private float rotationSpeed = 1f;
	private float minY = -60f;
	private float maxY = 60f;
	private float rotationY = 0f;
	private float rotationX = 0f;

    // Start is called before the first frame update
	void Start()
	{

		speed = 5.0f;
		rbody = GetComponent<Rigidbody>();

	}

    // Update is called once per frame
	void Update()
	{
		direction = Vector3.zero;
		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");
		direction = direction.normalized;
		if(direction.x != 0)
		rbody.MovePosition(rbody.position + transform.right * direction.x * speed * Time.deltaTime);
		if(direction.z != 0)
		rbody.MovePosition(rbody.position + transform.forward * direction.z * speed * Time.deltaTime);

		rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
		rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
		rotationY = Mathf.Clamp(rotationY, minY, maxY);
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
	}
}
