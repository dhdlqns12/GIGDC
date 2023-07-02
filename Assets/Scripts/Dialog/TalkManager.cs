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
        //대사 생성
        talkData.Add(100, new string[] { "인형\n일어낫니?", "인형\n무서운 꿈을 꾸었나보네 부모님을 찾으러 옆방으로 가보자" });
        talkData.Add(101, new string[] { "인형\n쥐들이 왜 이렇게 많지?", "인형\n빗자루로 쓸어 버리자!" });
        talkData.Add(102, new string[] { "인형\n부모님이 방에도 안계시네 서재에 가볼까?" });
        talkData.Add(103, new string[] { "인형\n갇혔어! 칼들을 피해서 열쇠를 가져오자." });
        talkData.Add(104, new string[] { "인형\n움직였더니 목이 마르네", "인형\n부모님도 찾을겸 1층 주방으로 가자!" });
        talkData.Add(105, new string[] { "인형\n부모님이 집에 안계시나봐", "인형\n삐삐도 보이지 않네...", "인형\n밖으로 나가서 찾아보자." });
        talkData.Add(106, new string[] { "인형\n새총이네... 일단 챙겨가보자." });
        talkData.Add(107, new string[] { "인형\n마당에도 아무도 없네...", "인형\n숲속으로 들어가서 찾아보자" });
        talkData.Add(108, new string[] { "인형\n저 앞에 나무에 열매가 있어.", "인형\n마침 배고픈데 열매가 열려있네 새총으로 따서먹자!" });
        talkData.Add(109, new string[] { "인형\n앗 늑대가 있어...", "인형\n숲 안으로 도망가자!" });
        talkData.Add(110, new string[] { "인형\n늑대를 따돌렷어 다행이야.", "인형\n어라 여긴 이웃집 아저씨 집이네!", "인형\n여기에 부모님이 계실까? 들어가 보자" });
        talkData.Add(111, new string[] { "인형\n뭐였지? 이상해! 빨리 나가자!" });
        talkData.Add(112, new string[] { "인형\n여기도 찾아보자!" });
        talkData.Add(113, new string[] { "인형\n또 갇혔어 여우들이 열쇠를 가지고 있나봐", "플레이어\n해치우자!" });
        talkData.Add(114, new string[] { "인형\n열쇠가 많네? 뭐가 맞을지 찾아보자!" });
        talkData.Add(115, new string[] { "인형\n여기도 부모님이랑 삐삐가 없네...", "인형\n삐삐가 호숫가를 좋아했으니까 호수로 가보자." });
        talkData.Add(116, new string[] { "인형\n다리가 이상해!", "플레이어\n일단 구멍피해서 가보자!." });
        talkData.Add(117, new string[] { "인형\n도끼가 있네, 챙겨가자." });
        talkData.Add(118, new string[] { "인형\n저 괴물은 뭐지? 괴물의 아래에 삐삐 목줄이 있어.", "플레이어\n저 괴물을 물리치자." });
        //초상화 생성
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
