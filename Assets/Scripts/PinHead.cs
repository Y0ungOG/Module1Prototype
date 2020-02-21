using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinHead : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PinHead")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }
}
