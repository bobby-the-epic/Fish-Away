using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float spawnPosX = 15;
    float spawnPosYStart = 3;
    float spawnPosYEnd = -6;
    float timeRemaining = 150;
    int minutes, seconds;
    string minutesText, secondsText;
    int poolSize = 25;
    GameOverMenu gameOverMenu;
    SpawnPos fishSpawnPos = SpawnPos.left;
    GameObject pooledObj;
    List<GameObject> fishPool;
    public static int score = 0;
    public static int highScore = 0;
    public static float volume = 0.5f;
    public static bool gameOver = false;
    public GameObject commonFish, rareFish, epicFish, legendaryFish;
    public TextMeshProUGUI scoreText, timerText;

    void Awake()
    {
        timeRemaining = 5;
        gameOver = false;
        gameOverMenu = gameObject.GetComponent<GameOverMenu>();
        PoolInit();
        scoreText.text = "Score: " + score;
        //todo: Make intervals random
        InvokeRepeating("SpawnFish", 3, 1);
    }
    void Update()
    {
        if (!gameOver)
        {
            RunTimer();
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    void RunTimer()
    {
        timeRemaining -= Time.deltaTime;
        seconds = (int)(timeRemaining % 60);
        minutes = (int)(timeRemaining / 60);
        if (minutes == 0)
            minutesText = "";
        else
            minutesText = minutes.ToString();
        if (seconds < 10)
            secondsText = "0" + seconds;
        else
            secondsText = seconds.ToString();
        timerText.text = minutesText + ":" + secondsText;
        if (timeRemaining <= 0)
        {
            gameOver = true;
            if (highScore < score)
                highScore = score;
            gameOverMenu.GameOver();
        }
    }
    void PoolInit()
    {
        /*
        Initializes the object pool.
        A group or "pool" of objects is instantiated and deactivated at game start. 
        They are enabled one by one when spawned and are deactivated when caught or they go
        off screen. You could just repeatedly instantiate and destroy objects, but this
        method is better for memory supposedly.
        */
        fishPool = new List<GameObject>();
        for (int counter = 0; counter < poolSize; counter++)
        {
            if (counter > 9 && counter < 15)
                pooledObj = Instantiate(rareFish, Vector3.zero, rareFish.transform.rotation);
            else if (counter > 14 && counter < 20)
                pooledObj = Instantiate(epicFish, Vector3.zero, epicFish.transform.rotation);
            else if (counter > 19)
                pooledObj = Instantiate(legendaryFish, Vector3.zero, legendaryFish.transform.rotation);
            else
                pooledObj = Instantiate(commonFish, Vector3.zero, commonFish.transform.rotation);
            pooledObj.SetActive(false);
            fishPool.Add(pooledObj);
            pooledObj.transform.SetParent(this.transform);
        }
    }
    void SpawnFish()
    {
        GameObject newFish = GetFish();
        if (newFish == null)
        {
            //For now, this does nothing, but I could make it to where Update checks for 
            //an available fish (or that just might be unnecessary).
            return;
        }
        Fish fishScript = newFish.GetComponent<Fish>();
        float yRange = Random.Range(spawnPosYStart, spawnPosYEnd);
        switch (fishSpawnPos)
        {
            case SpawnPos.left:
                newFish.transform.position = new Vector3(-spawnPosX, yRange);
                fishScript.spawnPos = fishSpawnPos;
                fishSpawnPos = SpawnPos.right;
                break;
            case SpawnPos.right:
                newFish.transform.position = new Vector3(spawnPosX, yRange);
                fishScript.spawnPos = fishSpawnPos;
                fishSpawnPos = SpawnPos.left;
                break;
        }
        newFish.SetActive(true);
    }
    GameObject GetFish()
    {
        //Generates the rarities of the fish. There's probably a better way of doing this.
        //This was the only way I knew of to generate weights for the rarity chances.

        int rng = Random.Range(1, 101);
        string rarity;

        //Between 1 and 70 inclusive, so 70% chance for a common fish.
        if (rng < 71)
            rarity = "Common";
        //Between 70 (exclusive) and 85 (inclusive), 15% chance for a rare fish.
        else if (rng > 70 && rng < 86)
            rarity = "Rare";
        //Between 85 (exclusive) and 95 (inclusive), 10 % chance for an epic fish.
        else if (rng > 85 && rng < 96)
            rarity = "Epic";
        //Range check was omitted since it was the last case, but it should be (95, 100].
        else
            rarity = "Legendary";
        /*
        Right now this iterates through the whole list, which is not a problem since
        the list is not that long. But, it would be better to check for the rarity first, then
        check if it is active in the hierarchy. Maybe make separate lists for each rarity?
        It would just make the PoolInit function a little longer.
        */
        for (int counter = 0; counter < poolSize; counter++)
        {
            GameObject fishInPool = fishPool[counter];
            if (!fishInPool.activeInHierarchy && fishInPool.CompareTag(rarity))
                return fishInPool;
        }
        return null;
    }
}
