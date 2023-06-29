using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    public Image healthPointBar;

    public BossController boss;


    private void Start()
    {

    }

    private void Update()
    {
        float HP = boss.health;
        healthPointBar.fillAmount = HP / 100f;
    }
}
