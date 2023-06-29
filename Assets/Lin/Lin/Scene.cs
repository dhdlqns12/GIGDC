using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public GameObject IMG;
    public void Change()
    {
        SceneManager.LoadScene("LS");
    }

    public void OnClick()
    {
        Application.Quit();
    }

    public void IMGClick()
    {
        IMG.SetActive(false);

        Time.timeScale = 1f;
    }
}
