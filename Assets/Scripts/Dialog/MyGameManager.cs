using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;
    public Image portraitImg;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            scanObject.SetActive(false);
            return;
        }
        if(isNpc)
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.sprite = talkManager.GetPortrait(id);
            portraitImg.color = new Color(1, 1, 1, 1);
        }

        isAction = true;
        talkIndex++;
    }
}
