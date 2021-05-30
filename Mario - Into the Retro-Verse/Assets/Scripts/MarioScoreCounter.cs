using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarioScoreCounter : MonoBehaviour
{
    public Text scoreText;
    public int coinScore;
    public int goombaScore;
    private int time;
    private bool canIncrease;
    private string timeString;

    public MarioSpace player;

    void Start()
    {
        goombaScore = 0;
        time = 300;
        canIncrease = true;
        StartCoroutine(KeepTime());
    }

    void Update()
    {
        if (time < 0)
        {
            time = 0;
            player.Death();
        } else if (time < 10)
        {
            timeString = " 00" + Mathf.Abs(time);
        } else if (time < 100)
        {
            timeString = " 0" + Mathf.Abs(time);
        } else 
        {
            timeString = " " + Mathf.Abs(time);
        }

        scoreText.text = "Mario                                                     World                        Time\n000"
            + goombaScore + "00                  x0"
            + coinScore + "                      1 - 1                             " + timeString;
    }

    IEnumerator KeepTime()
    {
        yield return new WaitForSeconds(1f);
        time--;
        StartCoroutine(KeepTime());
    }

    public void IncreaseCoin()
    {
        StartCoroutine(IncreaseScore());
    }

    IEnumerator IncreaseScore()
    {
        if (canIncrease)
        {
            canIncrease = false;
            coinScore++;
            yield return new WaitForSeconds(0.25f);
            canIncrease = true;
        }
    }
}
