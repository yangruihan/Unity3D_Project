using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour
{
    [SyncVar]
    public string pname = "player";

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            pname = GUI.TextField(new Rect(25, Screen.height - 40, 100, 30), pname);
            if (GUI.Button(new Rect(130, Screen.height - 40, 80, 30), "Change"))
            {
                CmdChangeName(pname);
            }
        }
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        pname = newName;
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<drive>().enabled = true;
            //Camera.main.transform.position = this.transform.position - this.transform.forward * 10 + this.transform.up * 3;
            //Camera.main.transform.LookAt(this.transform.position);
            //Camera.main.transform.parent = this.transform;
            SmoothCameraFollow.target = this.transform;
        }
    }

    void Update()
    {
        this.GetComponentInChildren<TextMesh>().text = pname;
    }
}
