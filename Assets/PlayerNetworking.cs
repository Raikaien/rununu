using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworking : Photon.MonoBehaviour
{
    //[SerializeField] private MonoBehaviour[] scriptsToIgnore;

    //PhotonView photonView;

    //private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
    //private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this
    // Start is called before the first frame update
    //void Start()
    //{
    //    //photonView = GetComponent<PhotonView>();

    //    //Initialize();
    //    if (!photonView.isMine)
    //    {
    //        transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
    //    }
    //}

    //void Initialize()
    //{
    //    if (photonView.isMine)
    //    {

    //    } else
    //    {
    //        foreach (MonoBehaviour item in scriptsToIgnore)
    //        {
    //            item.enabled = false;
    //        }
    //    }
    //}

    //void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.isWriting)
    //    {
    //        // We own this player: send the others our data
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);

    //        myThirdPersonController myC = GetComponent<myThirdPersonController>();
    //        stream.SendNext((int)myC._characterState);
    //    }
    //    else
    //    {
    //        // Network player, receive data
    //        this.correctPlayerPos = (Vector3)stream.ReceiveNext();
    //        this.correctPlayerRot = (Quaternion)stream.ReceiveNext();

    //        myThirdPersonController myC = GetComponent<myThirdPersonController>();
    //        myC._characterState = (CharacterState)stream.ReceiveNext();
    //    }
    //}


}
