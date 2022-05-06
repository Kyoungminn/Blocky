using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun.UtilityScripts;

public class PlayerManager : MonoBehaviour
{


    #region Public Fields
    public Transform head;
    public Transform left;
    public Transform right;

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    //public int playerNumber;
    #endregion

    #region Private Fields
    private PhotonView photonView;
    private Transform headRig;
    private Transform leftRig;
    private Transform rightRig;
    private TextMesh nameText;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        XRRig rig = FindObjectOfType<XRRig>();
        headRig = rig.transform.Find("Camera Offset/Main Camera");
        leftRig = rig.transform.Find("Camera Offset/LeftHand Controller");
        rightRig = rig.transform.Find("Camera Offset/RightHand Controller");
        nameText = transform.GetComponentInChildren<TextMesh>();
        nameText.text = photonView.Owner.NickName;

        if (photonView.IsMine)
        {
            /*foreach (var item in GetComponentsInChildren<Renderer>())
                {   
                     item.enabled = false;
                }*/

            //head.gameObject.SetActive(false);
            head.transform.GetChild(0).GetComponent<MeshFilter>().mesh = null;
            head.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);

            //left.gameObject.SetActive(false);
            //right.gameObject.SetActive(false);


            //Invoke("makePlayerNumber", 1f);

        }

    }

    /*void makePlayerNumber()
    {
        playerNumber = PhotonNetwork.LocalPlayer.GetPlayerNumber();
        Debug.Log(playerNumber);
    }*/

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            /*head.gameObject.SetActive(false);
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);*/

            nameText.gameObject.SetActive(false);

            mapPosition(head, headRig);
            mapPosition(left, leftRig);
            mapPosition(right, rightRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
        }
    }

    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void mapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }

}
