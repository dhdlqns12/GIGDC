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

        // ũ������ ���� ��ũ���մϴ�.
        rectTransform.anchoredPosition += Vector2.up * Speed * Time.deltaTime;

        // ũ������ ���ߴ� ��ġ�� �Ѿ�� ��ũ���� ����ϴ�.
        if (rectTransform.anchoredPosition.y > stopPositionY)
        {
            isScrolling = false;
        }
    }
}
