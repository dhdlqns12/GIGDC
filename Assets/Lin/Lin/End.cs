using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public float Speed;

    private RectTransform rectTransform;

    public float stopPositionY = 2400f;
    private bool isScrolling = true;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!isScrolling)
            return;

        // 크레딧을 위로 스크롤합니다.
        rectTransform.anchoredPosition += Vector2.up * Speed * Time.deltaTime;

        // 크레딧이 멈추는 위치를 넘어가면 스크롤을 멈춥니다.
        if (rectTransform.anchoredPosition.y > stopPositionY)
        {
            isScrolling = false;
        }
    }
}
