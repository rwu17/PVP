using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_ID : NetworkBehaviour {

    [SyncVar] private string playerUniqueName;
    private NetworkInstanceId playerNetID;
    private Transform myTransform;

    private Text playerFrameName;

    public string playerPublicName;

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
        playerPublicName = playerUniqueName;
        playerFrameName = GameObject.Find("PlayerName").GetComponent<Text>();
    }

    public string GetPlayerUniqueName()
    {
        return playerUniqueName;
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
