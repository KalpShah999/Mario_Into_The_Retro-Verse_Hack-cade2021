using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceLifeCounter : MonoBehaviour
{
    public MarioSpace player;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public Text lifeScore;

    void Start()
    {
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
    }

    void Update()
    {
        lifeScore.text = " " + player.health;

        switch(player.health)
        {
            case 4:
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                break;
            case 3:
                life1.SetActive(false);
                life2.SetActive(true);
                life3.SetActive(true);
                break;
            case 2:
                life1.SetActive(false);
                life2.SetActive(false);
                life3.SetActive(true);
                break;
            case 1:
                life1.SetActive(false);
                life2.SetActive(false);
                life3.SetActive(false);
                break;
        }
    }
}
