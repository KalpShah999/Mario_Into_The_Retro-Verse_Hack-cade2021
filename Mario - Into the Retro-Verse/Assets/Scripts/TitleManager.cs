using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public LevelChanger levelChanger;

    public AudioSource audioSouce;
    public AudioClip selectAudio;

    private bool canPlay;

    private void Start()
    {
        canPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && canPlay)
        {
            canPlay = false;
            audioSouce.clip = selectAudio;
            audioSouce.Play();
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        levelChanger.ToBlack();
        yield return new WaitForSecondsRealtime(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
