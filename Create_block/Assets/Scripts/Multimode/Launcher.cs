using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
	///region 1
	///룸에 참여할 수 있는 최대 플레이어 수를 
	///공개필드화해서 하드코딩하지 않고 인스펙터에서 수정할 수 있도록
	///방이름 받아오기
	#region Private Serializable Fields
	[SerializeField]
	private byte maxPlayersPerRoom = 4;
	[SerializeField]
	private Text RoomNameInputField;
	#endregion

	///region 2
	///룸에 들어갔다가 다시 로비로 나왔을 때, 바로 다시 join되지 않도록 제어
	///게임버전. 그냥 1로
	#region Private Fields
	bool isConnecting;
	string gameVersion = "1";
    #endregion

    #region Public Fields
	///패널 분리
	public enum ActivePanel
    {
		LOGIN = 0,
		LOBBY = 1,
		ROOMS = 2,
    }
	public GameObject[] panels;
    #endregion

    ///region 3
    #region MonoBehaviour CallBacks
    void Awake()
	{
		/// 모든 플레이어에게 로드되는 씬이 자동으로 동기화되도록
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

	//보여주는 패널 설정하기
	private void ChangePanel(ActivePanel panel)
	{
		foreach (GameObject _panel in panels)
		{
			Debug.Log(panels);
			_panel.SetActive(false);
		}
		panels[(int)panel].SetActive(true);
	}

	///single룸 선택시 그냥 룸 하나 만들어줌
	public void OnSingleRoomClick()
	{
		PhotonNetwork.CreateRoom(null
								, new RoomOptions { MaxPlayers = 1 });

	}

	//muti룸 선택시 방을 만들거나 들어가는 화면 보여줌
	public void OnMultiRoomClick()
    {
		ChangePanel(ActivePanel.ROOMS);
	}

	//enter클릭하면 방을 만들거나, 있는 방에 들어감
	public void OnEnterClick()
    {
		string roomName = RoomNameInputField.text.ToString();
		PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom },null);
    }
	#endregion


	///region 5
	#region MonoBehaviourPunCallbacks Callbacks
	///연결되면 랜덤 룸에 들어감 
	public override void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster() was called by PUN");
		if (isConnecting)
		{
			ChangePanel(ActivePanel.LOBBY);
		}
	}

    ///연결 안되면 오류로그 
    public override void OnDisconnected(DisconnectCause cause)
	{
		Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
		ChangePanel(ActivePanel.LOGIN);
	}
	///방이 없으면 만들음
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log(":OnJoinRandomFailed() was called by PUN. No random room available");
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
	}
	///방에 들어감 
	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
		if (PhotonNetwork.IsMasterClient)
		{
			Debug.Log(PhotonNetwork.CurrentRoom.Name+" im master");
			PhotonNetwork.LoadLevel("CreateScene");
		}
	}
	#endregion
}
