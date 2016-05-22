using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using AWFramework;

public class SettingsMenuManager : MonoBehaviour
{
	ScreenLogger logger;

	//network panel
	public AWNetworkManager networkManager;
	public InputField serverIpField;
	public InputField serverPortField;

	bool isConnected = false;

	// Init
	void Start ()
	{
		if(networkManager == null){
			string goName = AWConfig.AW_CONFIG_GO_NAME;
			networkManager = GameObject.Find(goName).GetComponent<AWNetworkManager>();
		}

		logger = ScreenLogger.getLogger ();
		initEvents();
	}

	void initEvents(){

		serverIpField.onEndEdit.AddListener(delegate(string arg0) {
			networkManager.networkAddress = arg0;
			Log ("new address setted: " + networkManager.networkAddress);
		});

		serverPortField.onEndEdit.AddListener(delegate(string arg0) {
			int number; 
			if(int.TryParse(arg0, out number)){
				networkManager.networkPort = number;
				Log ("new server port " + networkManager.networkPort);
			}
		});
	}

	void Log (string str)
	{
		str = "Settings] " + str;
		logger.ShowMsg (str);
	}

	public void OnClickStartServer(){
		if(!isConnected){
			networkManager.StartServer();
			Log (" starting host ");
			printConnectionInfo();
		}
		else{
			networkManager.StartServer();
			Log (" stopping host ");
		}
		isConnected = !isConnected;
	}

	public void OnClickConnectToServer(){
		if(!isConnected){
			networkManager.StartClient();
			Log (" starting client ");
			printConnectionInfo();
		}
		else{
			networkManager.StopClient();
			Log (" stopping client ");
		}
		isConnected = !isConnected;
	}

	void printConnectionInfo(){
		if (NetworkServer.active)
		{
			Log("Server: port=" + networkManager.networkPort);
		}
		if (NetworkClient.active)
		{
			Log("Client: address=" + networkManager.networkAddress + " port=" + networkManager.networkPort);
		}
	}
}
