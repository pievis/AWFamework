using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Basic Main Menu functionality.
 * Handles sub-menu toogle and other stuff.
 **/
public class MainMenuManager : MonoBehaviour
{
	//buttons
//	public Button settingsButton;

	//panels
	public GameObject settingsMenu;

	//other
	public GameObject[] onSubMenuHide;
	public GameObject[] onSubMenuShow;

	public void ToogleSettings ()
	{
		ToogleMenuView (settingsMenu);
	}

	public void CloseAll(){
		settingsMenu.SetActive(false);
		ShowUI ();
	}

	void ToogleMenuView (GameObject menu)
	{
		bool value = !menu.activeSelf;
		menu.SetActive (value);
		if(value)
			HideUI();
		else
			ShowUI();
	}

	void HideUI ()
	{
		if (onSubMenuHide == null)
			return;
		foreach (GameObject go in onSubMenuHide)
			go.SetActive (false);
		if (onSubMenuShow == null)
			return;
		foreach (GameObject go in onSubMenuShow)
			go.SetActive (true);
	}

	void ShowUI ()
	{
		if (onSubMenuHide == null)
			return;
		foreach (GameObject go in onSubMenuHide)
			go.SetActive (true);
		if (onSubMenuShow == null)
			return;

		foreach (GameObject go in onSubMenuShow)
			go.SetActive (false);
	}
}
