using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacles : MonoBehaviour
{
    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Player")
        {
            Destroy(Player.gameObject);
        }
    }
}
