using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public Text score;
    public GameObject food;

    string playerScore;
    public static int highScore;

    void FixedUpdate()
    {
        int temp = 156 - food.transform.childCount;
        if (temp < 10)
        {
            playerScore = " 00" + temp;
        }
        else if (temp < 100)
        {
            playerScore = " 0" + temp;
        }
        else
        {
            playerScore = " " + temp;
        }

        if (temp > highScore)
        {
            score.text = "1UP     " + playerScore + "0      HighScore: " + playerScore + "0";
            highScore = temp;
        }
        else
        {
            string temp2 = " " + highScore;
            if (highScore < 10)
            {
                temp2 = " 00" + highScore;
            }
            else if (highScore < 100)
            {
                temp2 = " 0" + highScore;
            }
            else
            {
                temp2 = " " + highScore;
            }
            score.text = "1UP     " + playerScore + "0      HighScore: " + temp2 + "0";
        }
    }
}
