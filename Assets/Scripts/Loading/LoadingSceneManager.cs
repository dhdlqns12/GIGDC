using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    float loadingTime;
    float loadingPercent;
    public Text loadingText;

    private void Update()
    {
        loadingTime += Time.deltaTime;

        if(loadingTime>=5.5f)
        {
            SceneManager.LoadScene("SampleScene");
        }
        if(loadingTime>=0.5f)
        {
            loadingPercent = 10;
        }
        if(loadingTime >= 1)
        {
            loadingPercent = 20;
        }
       if (loadingTime >= 1.5f)
        {
            loadingPercent = 30;
        }
        if (loadingTime >= 2)
        {
            loadingPercent = 40;
        }
        if (loadingTime >= 2.5f)
        {
            loadingPercent = 50;
        }
        if (loadingTime >= 3)
        {
            loadingPercent = 60;
        }
        if (loadingTime>=3.5f)
        {
            loadingPercent = 70;
        }
        if (loadingTime >= 4)
        {
            loadingPercent = 80;
        }
        if (loadingTime >= 4.5f)
        {
            loadingPercent = 90;
        }
        if (loadingTime >= 5)
        {
            loadingPercent = 100;
        }

        loadingText.text = loadingPercent + "%";

        Debug.Log(loadingTime);
    }
    
}
