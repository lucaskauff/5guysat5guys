using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuManager : MonoBehaviour
{
    [Header("Objects to serialize")]
    [SerializeField] SuperTextMesh numberOfGuysAlive = default;

    private void Start()
    {
        int guysLeft = FindObjectOfType<Container>().valueToStore;

        if (guysLeft == 1)
        {
            numberOfGuysAlive.text = "You managed to save: " + guysLeft + " guy";
        }
        else
        {
            numberOfGuysAlive.text = "You managed to save: " + guysLeft + " guys";
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            FindObjectOfType<Container>().valueToStore = 0;
            SceneManager.LoadScene("MainMenu");
        }
    }
}