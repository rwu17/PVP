using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSphere : NetworkBehaviour {
    void Update()
    {
        if (hasAuthority == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.transform.Translate(Vector3.up * Time.deltaTime * 3f);
            }
        }
    }
}
