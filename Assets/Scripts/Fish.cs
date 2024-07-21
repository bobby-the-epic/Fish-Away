using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Catchable
{
    float moveSpeed = 3;
    float xBound = 15;
    public SpawnPos spawnPos;

    void Awake()
    {
        /*
        I moved this to Awake from Start because the scores weren't being
        initialized immediately. Probably because the fish are initialized on 
        GameManager's Start and then immediately deactivated.
        */

        switch (gameObject.tag)
        {
            case "Common":
                score = 5;
                break;
            case "Rare":
                score = 10;
                moveSpeed = 4;
                break;
            case "Epic":
                score = 15;
                moveSpeed = 5;
                break;
            case "Legendary":
                score = 20;
                moveSpeed = 6;
                break;
        }
    }
    void FixedUpdate()
    {
        if (!caught)
        {
            switch (spawnPos)
            {
                case SpawnPos.left:
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                    if (transform.position.x > xBound)
                        gameObject.SetActive(false);
                    break;
                case SpawnPos.right:
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                    if (transform.position.x < -xBound)
                        gameObject.SetActive(false);
                    break;
            }
        }
    }
}
