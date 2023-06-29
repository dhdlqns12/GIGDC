using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCheck : MonoBehaviour
{
    public GameObject dialogCollider5;
    public GameObject dialogCollider8;

    public void Update()
    {
        if(GameManager.Instance.key==false)
        {
            dialogCollider5.SetActive(true);
        }
    }
}
