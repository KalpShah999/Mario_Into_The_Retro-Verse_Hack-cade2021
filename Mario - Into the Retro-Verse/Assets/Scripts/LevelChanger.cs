using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = transform.GetComponent<Animator>();
        if (!MarioSpace.world2 && SceneManager.GetActiveScene().name == "Pac-Man")
        {
            transform.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
        }
        else if (!MarioSpace.world3 && SceneManager.GetActiveScene().name == "SpaceInvader")
        {
            transform.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
        }
        else if (!MarioSpace.world4 && SceneManager.GetActiveScene().name == "MortalKombat")
        {
            transform.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
        }
    }

    public void ToBlack()
    {
        anim = transform.GetComponent<Animator>();
        anim.SetTrigger("ToBlack");
    }

    public void ToWhite()
    {
        anim = transform.GetComponent<Animator>();
        anim.SetTrigger("ToWhite");
    }
}
