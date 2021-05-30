using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MortalKeeper : MonoBehaviour
{
    public Text timerText;
    public Text timerTextShadow;
    private int timer;

    public Image marioHealth;
    public Image subZeroHealth;

    private int scoreAmount;
    private string scoreString;
    public Text score;
    public Text scoreShadow;

    public MarioSpace player;
    public SubZero subZero;

    void Start()
    {
        timer = 99;
        StartCoroutine(KeepTime());
    }

    void Update()
    {
        scoreAmount = (int)(1.5f * (150f - subZero.health));

        if (scoreAmount < 10)
        {
            scoreString = " 0000" + scoreAmount;
        } else if (scoreAmount < 100)
        {
            scoreString = " 000" + scoreAmount;
        } else if (scoreAmount < 1000)
        {
            scoreString = " 00" + scoreAmount;
        } else if (scoreAmount < 10000)
        {
            scoreString = " 0" + scoreAmount;
        }

        if (timer < 0)
        {
            timer = 0;
            if (player.health >= subZero.health)
            {
                subZero.isDead = true;
                subZero.canMove = false;
                subZero.headCollider.SetActive(true);
            }
            else if (subZero.health > player.health)
            {
                player.Death();
            }
        } else if (timer < 10)
        {
            timerText.text = "  0" + timer; 
        } else
        {
            timerText.text = "  " + timer;
        }

        marioHealth.fillAmount = player.health / 3f;
        subZeroHealth.fillAmount = subZero.health / 150f;
        timerTextShadow.text = timerText.text;
        score.text = " " + scoreString + "00";
        scoreShadow.text = score.text;
    }

    IEnumerator KeepTime()
    {
        yield return new WaitForSeconds(1f);
        timer--;
        StartCoroutine(KeepTime());
    }
}
