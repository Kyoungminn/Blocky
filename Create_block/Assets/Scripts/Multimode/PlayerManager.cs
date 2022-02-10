using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerManager : MonoBehaviour
{

    #region Public Fields
    public Transform head;
    public Transform left;
    public Transform right;
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

        //애니메이션 넣을 경우 밑에 setActive 부분 지우고
        /*
         * if(PhotonView.isMine)
         * {
         *  foreach (var item in GetComponentInChildren<Renderer>())
         *      {   
         *            item.enabled = false;
         *      }
         * }
         */

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            head.gameObject.SetActive(false);
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            nameText.gameObject.SetActive(false);

            mapPosition(head, headRig);
            mapPosition(left, leftRig);
            mapPosition(right, rightRig);
        }
    }

    void mapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }


}
