using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAction : MonoBehaviour
{
    public MyGameManager myGameManager;
    GameObject scanObject;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& scanObject!=null)
        {
            myGameManager.Action(scanObject);
        }
    }
}
