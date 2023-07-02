using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public GameObject IMG;
    public void Change()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("LS");
    }

    public void OnClick()
    {
        Application.Quit();
    }

    public void GMin()
    {
        SceneManager.LoadScene("GameMain");
    }


    public void IMGClick()
    {
        IMG.SetActive(false);

        Time.timeScale = 1f;
    }
}
