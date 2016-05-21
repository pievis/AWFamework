using UnityEngine;
using System.Collections;
using AWFramework;

public class CubeView : MonoBehaviour, IView
{

	Renderer rend;
	HologramComponent hc;
	int colorInt = 0;
	Rigidbody rb;

	//
	public Color[] colors;

	void Start ()
	{
		rend = GetComponent<Renderer> ();
		hc = GetComponent<HologramComponent> ();
		rb = GetComponent<Rigidbody>();
		if(colors == null || colors.Length == 0){
			colors = new[] {Color.blue, Color.red};
		}
		SetColor(colors[0]);
	}

	void Update ()
	{
		//Teporary View Control

		//SetColor test
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 100.0f)) {
				if (hit.collider.gameObject == gameObject) {
					SwitchColor();
				}
			}
		}

		//Rotate Test
		if (Input.GetKeyDown (KeyCode.A)) {
			hc.Invoke ("Rotate", Vector3.up, 10);
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			hc.Invoke ("Rotate", Vector3.down, 10);
		}

		Jumping ();
	}


	void SwitchColor(){
		colorInt = (colorInt + 1) % colors.Length;
		hc.Invoke("SetColor", colors[colorInt]);
	}

	bool isJumping = false;
	double jumpingTimer = 0;
	public double jumpingLimit = 1.5;

	void Jumping ()
	{
		if (isJumping) {
			jumpingTimer += Time.deltaTime;
			if (jumpingTimer > jumpingLimit) {
				isJumping = false;
				jumpingTimer = 0;
				if(rb != null)
					rb.isKinematic = false;
			}
			transform.position += Vector3.up * Time.deltaTime;
		}
	}

	//shared hologram methods

	public void Rotate (Vector3 axis, float degree)
	{
		transform.Rotate (axis, degree);
	}

	public void SetColor (Color color)
	{
		if (rend != null) {
			rend.material.SetColor ("_Color", color);
			Log ("color changed to " + color.ToString ());
		} else {
			Log ("renderer not found");
		}
	}

	public void SetColor (int colorInt)
	{
		Color color = colors[colorInt];
		SetColor (color);
	}

	public void Jump ()
	{
		isJumping = true;
		if(rb != null)
			rb.isKinematic = true;
	}

	void Log (string str)
	{
		Debug.Log ("CubeView] " + str);
	}
}
