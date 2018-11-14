using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    [SerializeField] private Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ClickTarget()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D 
        }
    }

}
