using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public GameObject dialogCollider;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }


    void GenerateData()
    {
        talkData.Add(100, new string[] { "����\n�Ͼ��?","�÷��̾�\n��... ������ ���� �پ��� ���� �θ���� ��ô� �������� ����."});
        talkData.Add(101, new string[] { "�÷��̾�\n����� �� �̷��� ����?","�÷��̾�\n���ڷ�� ���� ������!"});
        talkData.Add(102, new string[] { "�÷��̾�\n�θ���� �濡�� �Ȱ�ó� ���翡 ������?"});
        talkData.Add(103, new string[] { "�÷��̾�\n������! Į���� ���ؼ� ���踦 ��������." });
        talkData.Add(104, new string[] { "�÷��̾�\n���������� ���� ������","�÷��̾�\n�θ�Ե� ã���� 1�� �ֹ����� ����!" });
        talkData.Add(105, new string[] { "�÷��̾�\n�θ���� ���� �Ȱ�ó���", "�÷��̾�\n�߻ߵ� ������ �ʳ�...","�÷��̾�\n������ ������ ã�ƺ���." });
        talkData.Add(106, new string[] { "�÷��̾�\n�����̳�... �ϴ� ì�ܰ�����."});
        talkData.Add(107, new string[] { "�÷��̾�\n���翡�� �ƹ��� ����...","�÷��̾�\n�������� ���� ã�ƺ���" });
        talkData.Add(108, new string[] { "�÷��̾�\n�� �տ� ������ ���Ű� �־�.", "�÷��̾�\n��ħ ����µ� ���Ű� �����ֳ� �������� ��������!" });
        talkData.Add(109, new string[] { "�÷��̾�\n�� ���밡 �־�...", "�÷��̾�\n�� ������ ��������!" });
        talkData.Add(110, new string[] { "�÷��̾�\n���븦 �����Ǿ� �����̾�.", "�÷��̾�\n��� ���� �̿��� ������ ���̳�!","�÷��̾�\n���⿡ �θ���� ��Ǳ�? �� ����" });
        talkData.Add(111, new string[] { "�÷��̾�\n������? �̻���! ���� ������!" });
        talkData.Add(112, new string[] { "�÷��̾�\n���⵵ ã�ƺ���!" });
        talkData.Add(113, new string[] { "�÷��̾�\n�� ������ ������� ���踦 ������ �ֳ���","�÷��̾�\n��ġ����!"});
        talkData.Add(114, new string[] { "�÷��̾�\n���谡 ����? ���� ������ ã�ƺ���!"});
        talkData.Add(115, new string[] { "�÷��̾�\n���⵵ �θ���̶� �߻߰� ����...","�÷��̾�\n�߻߰� ȣ������ ���������ϱ� ȣ���� ������." });
        talkData.Add(116, new string[] { "�÷��̾�\n�ٸ��� �̻���!", "�÷��̾�\n�ϴ� �������ؼ� ������!." });
        talkData.Add(117, new string[] { "�÷��̾�\n������ �ֳ�, ì�ܰ���." });
        talkData.Add(118, new string[] { "�÷��̾�\n�� ������ ����? ������ �Ʒ��� �߻� ������ �־�.","�÷��̾�\n�� ������ ����ġ��." });
    }

    public string GetTalk(int id,int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            dialogCollider.SetActive(false);
            return null;
        }
        else
            return talkData[id][talkIndex];
    }
}
