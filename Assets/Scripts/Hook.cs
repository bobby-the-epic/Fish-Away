using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public bool objectCaught = false;
    public GameObject gmObj;
    GameManager gameManager;
    GameObject caughtObject;
    Catchable caughtObjScript;

    void Start()
    {
        gameManager = gmObj.GetComponent<GameManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!objectCaught)
        {
            caughtObject = collision.gameObject;
            caughtObjScript = caughtObject.GetComponent<Catchable>();
            caughtObjScript.caught = true;
            objectCaught = true;
        }
    }
    void FixedUpdate()
    {
        if (objectCaught)
        {
            caughtObject.transform.position = transform.position;
        }
    }
    public void CatchFish()
    {
        objectCaught = false;
        gameManager.UpdateScore(caughtObjScript.score);
        caughtObject.SetActive(false);
        caughtObjScript.caught = false;
        caughtObject = null;
    }
}
