using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    public Image image;
    bool isActive=false;

    private void OnEnable()
    {
        isActive = true;
        StartCoroutine(FadeOut(7));
        Invoke("GameEnd", 7.5f);
    }

    public IEnumerator FadeOut(float time)
    {
        Color color = image.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / time;
            image.color = color;
            yield return null;
        }
    }

    void GameEnd()
    {
        SceneManager.LoadScene("GameEnd");
    }
}
