using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerController : MonoBehaviour
{
    [Header("Public variables")]
    public int nbOfDir = 4;
    public float changingDirectionDelay = 1;
    public float stepRange = 1;
    public float moveSpeed = 1;
    public bool isArrived = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Guy")
        {
            KillTheGuy(collision.gameObject);
        }
    }

    void KillTheGuy(GameObject focusedGuy)
    {
        focusedGuy.GetComponent<GuysController>().GuyIsDead();
        Destroy(gameObject);
    }
}