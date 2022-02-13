using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class MultiGameManager : MonoBehaviourPunCallbacks
{
	#region Public Variables
	public GameObject playerPrefab;
	#endregion

	///게임 시작할 때 플레이어 프리팹 생성시켜줌 
	#region MonoBehaviour CallBacks
	void Start()
	{
		if (playerPrefab == null)
		{
			Debug.LogError("Missing playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
		}
		else
		{
			Debug.LogFormat("We are Instantiating LocalPlayer");
			PhotonNetwork.Instantiate(this.playerPrefab.name, transform.position, transform.rotation);
		}
	}
    #endregion

    //방에 나가면 로비씬(0)을 로드함
    #region Photon Callbacks

    public override void OnLeftRoom()
	{
		PhotonNetwork.Destroy(playerPrefab);
		SceneManager.LoadScene(0);

	}
	#endregion

	//방을 떠남
	#region Public Methods
	public void LeaveRoom()
	{
		PhotonNetwork.Destroy(playerPrefab);
		PhotonNetwork.LeaveRoom();
	}
	#endregion
}
