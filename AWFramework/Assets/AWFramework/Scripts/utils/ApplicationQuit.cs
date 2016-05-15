using UnityEngine;
using System.Collections;

public class ApplicationQuit : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
