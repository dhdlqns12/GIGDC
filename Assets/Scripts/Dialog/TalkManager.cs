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
        talkData.Add(116, new string[] { "인형\n다리가 이상해!", "플레이어\n일단 불길피해서 가보자!." });
        talkData.Add(117, new string[] { "인형\n도끼가 있네, 챙겨가자." });
        talkData.Add(118, new string[] { "인형\n저 괴물은 뭐지? 괴물의 아래에 삐삐 목줄이 있어.", "플레이어\n저 괴물을 물리치자." });
        talkData.Add(119, new string[] { "플레이어\n이게 코코였다고?", "플레이어\n그럴리가 없어!" });
        talkData.Add(120, new string[] { "플레이어\n어.. 이건?", "플레이어\n아빠 작업실 열쇠네 어서 집으로 돌아가 보자." });
        talkData.Add(121, new string[] { "플레이어\n뭔가 분위기가 달라진거 같아..." });
        talkData.Add(122, new string[] { "플레이어\n왜 이렇게 된거지?" });
        talkData.Add(123, new string[] { "플레이어\n다들 어디 간거지?","플레이어\n엄마... 아빠... 코코..." });
        talkData.Add(124, new string[] { "플레이어\n집도 타버렸어...", "플레이어\n아빠 작업실로 가보자." });
        talkData.Add(125, new string[] { "xx월 xx일\n이번 폭발 실험만 성공적으로 마치면 부자야","xx월 xx일\n왜 생각대로 안되는 거지? 젠장...","xx월 xx일\n화력을 더 높여 봐야겠어!","xx월 xx일\n좋아! 이론상 완벽해! 내일 실험해 보자!" });
        talkData.Add(126, new string[] { "플레이어\n맞아 큰 소리가 들리고 마을과 숲이 불탔어","플레이어\n다들 그 이후로 볼 수 없었어...","플레이어\n혼자 살아남았었지..." });
        talkData.Add(127, new string[] { "플레이어\n외롭네...","플레이어\n편해지고 싶어..." });
        talkData.Add(128, new string[] { "플레이어\n나무랑 집이 타있어..." });
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
