using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager_Custom : NetworkManager {

    public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIPAdress").transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            StartCoroutine(SetupMenuSceneButton());
            //SetupMenuSceneButton();
        }
        else
        {
            SetupOtherSceneButton();
        }
    }

    IEnumerator SetupMenuSceneButton()
    {
        yield return new WaitForSeconds(0.3f);

        Cursor.visible = true;

        GameObject.Find("HostStartButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("HostStartButton").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("JoinStartButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("JoinStartButton").GetComponent<Button>().onClick.AddListener(JoinGame);
    }

    void SetupOtherSceneButton()
    {
        GameObject.Find("QuitGameButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("QuitGameButton").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }
}
