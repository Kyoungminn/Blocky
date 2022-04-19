using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
	///region 1
	#region Private Serializable Fields
	[SerializeField]
	private byte maxPlayersPerRoom = 4;
	[SerializeField]
	private Text FindRoomNameInputField;
	[SerializeField]
	private Text NewRoomNameInputField;
	[SerializeField]
	private Text PlayerNameInputField;
	#endregion

	///region 2

	#region Private Fields
	bool isConnecting;
	string gameVersion = "1";
	#endregion

	#region Public Fields
	///�г� �и�
	public enum ActivePanel
	{
		LOGIN = 0,
		LOBBY = 1,
		SINGLE = 2,
		LOAD = 3,
		MULTI = 4,
		MULTIFIND = 5,
		MULTINEW = 6,
		MULTICHOOSE = 7,
		MULTISETTING = 8,
		WARNING =9,
	}
	public GameObject[] panels;
    #endregion

    ///region 3
    #region MonoBehaviour CallBacks
    void Awake()
	{
		
		PhotonNetwork.AutomaticallySyncScene = true;
	}
	void Start()
	{
		ChangePanel(ActivePanel.LOGIN);
	}
	#endregion

	///region 4
	#region Public Methods
	public void Connect()
	{
		PhotonNetwork.GameVersion = gameVersion;
		PhotonNetwork.ConnectUsingSettings();
		isConnecting = true;
	}

	
	private void ChangePanel(ActivePanel panel)
	{
		foreach (GameObject _panel in panels)
		{
			Debug.Log(panels);
			_panel.SetActive(false);
		}
		panels[(int)panel].SetActive(true);
	}


	public void OnSingleRoomClick()
	{
		ChangePanel(ActivePanel.SINGLE);
	}

	public void OnMultiRoomClick()
	{
		ChangePanel(ActivePanel.MULTI);
	}

	public void OnLoadClick()
	{
		ChangePanel(ActivePanel.LOAD);
	}

	//싱글 패널(로드, 뉴)
	public void OnNewClick()
	{
		//PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 1 });
		//임시로 바꿔둠(멀티 확인 위해)
		PlayerPrefs.SetInt("Custom", 0);
		PhotonNetwork.JoinOrCreateRoom("1234", new RoomOptions { MaxPlayers = maxPlayersPerRoom }, null);
	}

	public void OnFindClick()
	{
		ChangePanel(ActivePanel.MULTIFIND);
	}

	public void OnMultiNewClick()
	{
		ChangePanel(ActivePanel.MULTINEW);
	}

	//찾아서 방 들어가기
	public void OnEnterClick()
	{
		string roomName = FindRoomNameInputField.text.ToString();
		PhotonNetwork.JoinRoom(roomName);
	}

	//멀티방 새로 만들기 
	public void OnNewEnterClick()
	{
		ChangePanel(ActivePanel.MULTICHOOSE);

	}

	//자유모드 바로 만들기
	public void OnFreeModeClick()
	{
		PlayerPrefs.SetInt("Custom", 0);
		string roomName = NewRoomNameInputField.text.ToString();
		PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom }, null);
	}

	//커스텀 모드 선택 후 셋팅창으로 넘어가기
	public void OnCustomModeClick()
	{
		ChangePanel(ActivePanel.MULTISETTING);
	}

	//커스텀 모드 셋팅하고 방 만들기
	public void OnCustomModeCompleteClick()
	{
		PlayerPrefs.SetInt("Custom", 1);
		string roomName = NewRoomNameInputField.text.ToString();
		PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom }, null);
	}
	#endregion



	#region MonoBehaviourPunCallbacks Callbacks

	public override void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster() was called by PUN");
		if (isConnecting)
		{
			ChangePanel(ActivePanel.LOBBY);
			Debug.Log(PhotonNetwork.NetworkingClient.NickName + "here");
		}
	}


    public override void OnDisconnected(DisconnectCause cause)
	{
		Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
		ChangePanel(ActivePanel.LOGIN);
	}

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		Debug.Log(":OnJoinRoomFailed() was called by PUN. No room available");
		ChangePanel(ActivePanel.WARNING);
		//경고 문구 출력 
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
		if (PhotonNetwork.IsMasterClient)
		{
			Debug.Log(PhotonNetwork.CurrentRoom.Name+" im master");
			PhotonNetwork.LoadLevel("JoinScene");
		}
	}
	#endregion
}
