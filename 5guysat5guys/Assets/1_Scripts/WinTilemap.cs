using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTilemap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Guy")
        {
            collision.gameObject.GetComponent<GuysController>().GetsOut();
        }
    }
}