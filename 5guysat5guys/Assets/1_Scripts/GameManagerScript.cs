using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [Header("Objects to serialize")]
    [SerializeField] GuysController[] guys = default;

    //Private
    int activeGuy = 0;

    private void Start()
    {
        guys[activeGuy].GuyActivation(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SwitchGuy();
        }
    }

    void SwitchGuy()
    {
        int switchedGuys = 0;

        while (switchedGuys <= guys.Length)
        {
            if (activeGuy + 1 == guys.Length)
            {
                if (!guys[0].hasPlayed)
                {
                    guys[0].GuyActivation(true);
                    guys[activeGuy].GuyActivation(false);
                    activeGuy = 0;
                    return;
                }
                else
                {
                    activeGuy = 0;
                    switchedGuys += 1;
                }
            }
            else
            {
                if (!guys[activeGuy + 1].hasPlayed)
                {
                    guys[activeGuy + 1].GuyActivation(true);
                    guys[activeGuy].GuyActivation(false);
                    activeGuy += 1;
                    return;
                }
                else
                {
                    activeGuy += 1;
                    switchedGuys += 1;
                }
            }
        }

        ResetGuys();
    }

    void ResetGuys()
    {
        foreach (var guy in guys)
        {
            guy.hasPlayed = false;
        }
    }
}