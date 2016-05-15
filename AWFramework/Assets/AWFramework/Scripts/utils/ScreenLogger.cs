using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using AWFramework;

//Used as a debugging utility
public class ScreenLogger : MonoBehaviour
{
	//
	const int MAX_MSGS = 5;
	//
	List<string> msgs;
	List<float> timers;
	public bool showDebugText = true;
	//
	public float messageOnScreenTime = 2.0f;
	public Text loggerTxt;

	void Awake ()
	{
		msgs = new List<string> ();
		timers = new List<float> ();
	}

	void Update ()
	{
		Test ();
		UpdateMsgsOnScreen ();
	}

	//Show eash message on screen
	private void UpdateMsgsOnScreen ()
	{
		string text = "";
		foreach (string msg in msgs) {
			text += msg + "\n";
		}
		loggerTxt.text = text;
		UpdateTimers ();
	}

	private void UpdateTimers ()
	{
		if (msgs.Count == 0)
			return;
		if (Time.time - timers [0] > messageOnScreenTime) {
			timers.RemoveAt (0);
			msgs.RemoveAt (0); //remove the head
		}
	}

	public void ShowMsg (string msg)
	{
		if (showDebugText) {
			msgs.Add (msg);
			timers.Add (Time.time);
		}
	}

	static ScreenLogger instance;

	public static ScreenLogger getLogger ()
	{
		if (instance == null) {
			string configName = AWConfig.AW_CONFIG_GO_NAME;
			instance = GameObject.Find (configName).GetComponent<ScreenLogger> ();
		}
		return instance;
	}
	

	//for testing only
	private void Test ()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow))
			ShowMsg ("Input: Up pressed");
		if (Input.GetKeyDown (KeyCode.DownArrow))
			ShowMsg ("Input: Down pressed");
	}
}
