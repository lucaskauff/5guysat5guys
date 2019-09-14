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
        guys[activeGuy].ReactivateGuy();
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
    }
}