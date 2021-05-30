using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceCounter : MonoBehaviour
{
    public Text score;
    public GameObject enemies;

    string playerScore;
    public static int highScore;

    void FixedUpdate()
    {
        int temp = (44 - enemies.transform.childCount) * 2;
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
            score.text = "Score<1>    Highscore    Score <2>\n         "
                + playerScore + "0                        " + playerScore + "0                        0000";
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
            score.text = "Score<1>    Highscore    Score<2>\n         "
                + playerScore + "0                        " + temp2 + "0                        0000";
        }
    }
}
