using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_ID : NetworkBehaviour {

    [SyncVar] public string playerUniqueName;
    private NetworkInstanceId playerNetID;
    private Transform myTransform;

    public string playerFrameName;

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
        playerFrameName = GameObject.Find("PlayerName").GetComponent<Text>().text.ToString();
        //GameObject.Find("PlayerName").GetComponent<Text>().text = playerUniqueName;
    }

    void Awake()
    {
        myTransform = transform;
    }

    void Update()
    {
        if(myTransform.name == "" || myTransform.name == "Player(Clone)")
        {
            SetIdentity();
        }
    }

    [Client]
    void GetNetIdentity()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            myTransform.name = playerUniqueName;
        }
        else
        {
            myTransform.name = MakeUniqueIdentity();
        }
    }

    string MakeUniqueIdentity()
    {
        string uniqueName = "Player" + playerNetID.ToString();
        return uniqueName;
    }

    [Command]
    void CmdTellServerMyIdentity(string name)
    {
        playerUniqueName = name;
    }
}
