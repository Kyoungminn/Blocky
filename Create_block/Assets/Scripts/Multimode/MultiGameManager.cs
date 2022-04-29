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
	public int playerNumber;
	public Material[] materials;
	public GameObject[] hands;
	public GameObject currentPlayer;
	#endregion

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
			currentPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, transform.position, transform.rotation);
			Invoke("makePlayerNumber", 1f);

			//Debug.Log(playerPrefab.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial);

		}
	}
	public Material ColorSetting(int num)
	{
		if (num == 0)
		{
			return materials[0]; //red
		}
		else if (num == 1)
		{
			return materials[1]; //purple

		}
		else if (num == 2)
		{
			return materials[2]; //blue
		}
		else
		{
			return materials[3]; //yellow
		}
	}
	#endregion

	#region Photon Callbacks
	void makePlayerNumber()
	{
		playerNumber = PhotonNetwork.LocalPlayer.GetPlayerNumber();
		Debug.Log("prefab:" + playerNumber);

		currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = ColorSetting(playerNumber);
		currentPlayer.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial = ColorSetting(playerNumber);
		currentPlayer.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial = ColorSetting(playerNumber);
	}

	public override void OnLeftRoom()
	{
		PhotonNetwork.Destroy(playerPrefab);
		SceneManager.LoadScene(0);

	}
	#endregion

	#region Public Methods
	public void LeaveRoom()
	{
		PhotonNetwork.Destroy(playerPrefab);
		PhotonNetwork.LeaveRoom();
	}
	#endregion
}
