using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [Header("Objects to serialize")]
    [SerializeField] GuysController[] guys = default;

    //Private
    int activeGuy = 0;
    int checkNbGuysLeftToPlay = 0;
    int checknNbGuysDead = 0;
    bool lastGuySwitched = false;

    Container theContainer;

    private void Start()
    {
        theContainer = FindObjectOfType<Container>();
        guys[activeGuy].GuyActivation(true);
    }

    private void Update()
    {
        checkNbGuysLeftToPlay = 0;

        foreach (var guy in guys)
        {
            if (!guy.hasPlayed)
            {
                checkNbGuysLeftToPlay += 1;
            }

            if (!guy.isAlive)
            {
                checknNbGuysDead += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !lastGuySwitched)
        {
            SwitchGuy();
        }

        if (checkNbGuysLeftToPlay == 0)
        {
            ResetGuys();
        }
        
        if (checknNbGuysDead == guys.Length)
        {
            GameOver();
        }
        
        //Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameOver();
        }
    }

    void SwitchGuy()
    {
        int switches = 0;
        GuysController[] switchedGuys = new GuysController[guys.Length];
        
        checkNbGuysLeftToPlay = 0;
        foreach (var guy in guys)
        {
            if (!guy.hasPlayed)
            {
                checkNbGuysLeftToPlay += 1;
            }
        }
        
        if (checkNbGuysLeftToPlay > 1)
        {
            while (switches < guys.Length)
            {
                if (activeGuy + 1 == guys.Length)
                {
                    if (!guys[0].hasPlayed)
                    {
                        guys[0].GuyActivation(true);
                        guys[activeGuy].GuyActivation(false);

                        foreach (var switchedGuy in switchedGuys)
                        {
                            if (switchedGuy) switchedGuy.GuyActivation(false);
                        }

                        activeGuy = 0;
                        return;
                    }
                    else
                    {
                        switchedGuys[switches] = guys[activeGuy];
                        activeGuy = 0;
                        switches += 1;
                    }
                }
                else
                {
                    if (!guys[activeGuy + 1].hasPlayed)
                    {
                        guys[activeGuy + 1].GuyActivation(true);
                        guys[activeGuy].GuyActivation(false);

                        foreach (var switchedGuy in switchedGuys)
                        {
                            if (switchedGuy) switchedGuy.GuyActivation(false);
                        }

                        activeGuy += 1;
                        return;
                    }
                    else
                    {
                        switchedGuys[switches] = guys[activeGuy];
                        activeGuy += 1;
                        switches += 1;
                    }
                }
            }
        }
        else if (checkNbGuysLeftToPlay == 1)
        {
            if (activeGuy + 1 == guys.Length)
            {
                guys[0].GuyActivation(true);
                guys[activeGuy].GuyActivation(false);
                activeGuy = 0;
            }
            else
            {
                guys[activeGuy + 1].GuyActivation(true);
                guys[activeGuy].GuyActivation(false);
                activeGuy += 1;
            }

            lastGuySwitched = true;
        }
    }

    void ResetGuys()
    {
        foreach (var guy in guys)
        {
            guy.hasPlayed = false;
            guy.GetComponent<SpriteRenderer>().color = Color.white;
        }

        lastGuySwitched = false;
    }

    void GameOver()
    {
        theContainer.valueToStore = guys.Length - checknNbGuysDead;
        SceneManager.LoadScene("WinMenu");
    }
}