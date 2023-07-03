using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public GameObject dialogCollider;
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }


    void GenerateData()
    {
        //��� ����
        talkData.Add(100, new string[] { "����\n�Ͼ��?", "����\n������ ���� �پ������� �θ���� ã���� �������� ������" });
        talkData.Add(101, new string[] { "����\n����� �� �̷��� ����?", "����\n���ڷ�� ���� ������!" });
        talkData.Add(102, new string[] { "����\n�θ���� �濡�� �Ȱ�ó� ���翡 ������?" });
        talkData.Add(103, new string[] { "����\n������! Į���� ���ؼ� ���踦 ��������." });
        talkData.Add(104, new string[] { "����\n���������� ���� ������", "����\n�θ�Ե� ã���� 1�� �ֹ����� ����!" });
        talkData.Add(105, new string[] { "����\n�θ���� ���� �Ȱ�ó���", "����\n�߻ߵ� ������ �ʳ�...", "����\n������ ������ ã�ƺ���." });
        talkData.Add(106, new string[] { "����\n�����̳�... �ϴ� ì�ܰ�����." });
        talkData.Add(107, new string[] { "����\n���翡�� �ƹ��� ����...", "����\n�������� ���� ã�ƺ���" });
        talkData.Add(108, new string[] { "����\n�� �տ� ������ ���Ű� �־�.", "����\n��ħ ����µ� ���Ű� �����ֳ� �������� ��������!" });
        talkData.Add(109, new string[] { "����\n�� ���밡 �־�...", "����\n�� ������ ��������!" });
        talkData.Add(110, new string[] { "����\n���븦 �����Ǿ� �����̾�.", "����\n��� ���� �̿��� ������ ���̳�!", "����\n���⿡ �θ���� ��Ǳ�? �� ����" });
        talkData.Add(111, new string[] { "����\n������? �̻���! ���� ������!" });
        talkData.Add(112, new string[] { "����\n���⵵ ã�ƺ���!" });
        talkData.Add(113, new string[] { "����\n�� ������ ������� ���踦 ������ �ֳ���", "�÷��̾�\n��ġ����!" });
        talkData.Add(114, new string[] { "����\n���谡 ����? ���� ������ ã�ƺ���!" });
        talkData.Add(115, new string[] { "����\n���⵵ �θ���̶� �߻߰� ����...", "����\n�߻߰� ȣ������ ���������ϱ� ȣ���� ������." });
        talkData.Add(116, new string[] { "����\n�ٸ��� �̻���!", "�÷��̾�\n�ϴ� �ұ����ؼ� ������!." });
        talkData.Add(117, new string[] { "����\n������ �ֳ�, ì�ܰ���." });
        talkData.Add(118, new string[] { "����\n�� ������ ����? ������ �Ʒ��� �߻� ������ �־�.", "�÷��̾�\n�� ������ ����ġ��." });
        talkData.Add(119, new string[] { "�÷��̾�\n�̰� ���ڿ��ٰ�?", "�÷��̾�\n�׷����� ����!" });
        talkData.Add(120, new string[] { "�÷��̾�\n��.. �̰�?", "�÷��̾�\n�ƺ� �۾��� ����� � ������ ���ư� ����." });
        talkData.Add(121, new string[] { "�÷��̾�\n���� �����Ⱑ �޶����� ����..." });
        talkData.Add(122, new string[] { "�÷��̾�\n�� �̷��� �Ȱ���?" });
        talkData.Add(123, new string[] { "�÷��̾�\n�ٵ� ��� ������?","�÷��̾�\n����... �ƺ�... ����..." });
        talkData.Add(124, new string[] { "�÷��̾�\n���� Ÿ���Ⱦ�...", "�÷��̾�\n�ƺ� �۾��Ƿ� ������." });
        talkData.Add(125, new string[] { "xx�� xx��\n�̹� ���� ���踸 ���������� ��ġ�� ���ھ�","xx�� xx��\n�� ������� �ȵǴ� ����? ����...","xx�� xx��\nȭ���� �� ���� ���߰ھ�!","xx�� xx��\n����! �̷л� �Ϻ���! ���� ������ ����!" });
        talkData.Add(126, new string[] { "�÷��̾�\n�¾� ū �Ҹ��� �鸮�� ������ ���� ������","�÷��̾�\n�ٵ� �� ���ķ� �� �� ������...","�÷��̾�\nȥ�� ��Ƴ��Ҿ���..." });
        talkData.Add(127, new string[] { "�÷��̾�\n�ܷӳ�...","�÷��̾�\n�������� �;�..." });
        talkData.Add(128, new string[] { "�÷��̾�\n������ ���� Ÿ�־�..." });
        //�ʻ�ȭ ����
        portraitData.Add(100, portraitArr[0]);
        portraitData.Add(101, portraitArr[0]);
        portraitData.Add(102, portraitArr[0]);
        portraitData.Add(103, portraitArr[0]);
        portraitData.Add(104, portraitArr[0]);
        portraitData.Add(105, portraitArr[0]);
        portraitData.Add(106, portraitArr[0]);
        portraitData.Add(107, portraitArr[0]);
        portraitData.Add(108, portraitArr[0]);
        portraitData.Add(109, portraitArr[0]);
        portraitData.Add(110, portraitArr[0]);
        portraitData.Add(111, portraitArr[0]);
        portraitData.Add(112, portraitArr[0]);
        portraitData.Add(113, portraitArr[0]);
        portraitData.Add(114, portraitArr[0]);
        portraitData.Add(115, portraitArr[0]);
        portraitData.Add(116, portraitArr[0]);
        portraitData.Add(117, portraitArr[0]);
        portraitData.Add(118, portraitArr[0]);
        portraitData.Add(119, portraitArr[1]);
        portraitData.Add(120, portraitArr[1]);
        portraitData.Add(121, portraitArr[1]);
        portraitData.Add(122, portraitArr[1]);
        portraitData.Add(123, portraitArr[1]);
        portraitData.Add(124, portraitArr[1]);
        portraitData.Add(126, portraitArr[1]);
        portraitData.Add(127, portraitArr[1]);
        portraitData.Add(128, portraitArr[1]);
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
    public Sprite GetPortrait(int id)
    {
        return portraitData[id];
    }
}
