using UnityEngine;
using System.Collections;

public class MovingCameraSimple : MonoBehaviour
{

	public float rotSpeed = 2.0f;
	public float movingSpeed = 2.0f;
	float pitch;
	float yaw;
	Camera cam;

	// Use this for initialization
	void Start ()
	{
		cam = GetComponent<Camera> (); 
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (cam.isActiveAndEnabled) {
			Rotate ();
			Move ();
		}
	}

	void Rotate ()
	{
		yaw += Input.GetAxisRaw("Mouse X") * rotSpeed;
		pitch+= Input.GetAxisRaw("Mouse Y") * rotSpeed * -1;
		if (yaw >= 360)
			yaw -= 360;
		pitch = Mathf.Clamp (pitch, -90, 90);
		transform.eulerAngles = new Vector3 (pitch, yaw, 0f);
	}

	void Move ()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		Vector3 mov = new Vector3 (h, 0, v);
		transform.Translate(mov * Time.deltaTime * movingSpeed);
//		transform.position += mov * Time.deltaTime * movingSpeed;
	}
}
