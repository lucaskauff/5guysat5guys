using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] GuysController[] guys = default;

    private void Start()
    {
        //debug turn on and focus on guy0
        guys[0].canMove = true;
    }
}