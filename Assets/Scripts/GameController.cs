using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public PlayerController player;
    public GameObject otherClownsPrefab;
    public GameObject[] obstaclePrefabs;
    public int startingObstacleNumber;
    public GameObject cloneCarsPrefab;
    public GameObject fakeCloneCarPrefab;
    public float cloneCarSpawnPoint;
    public float cloneCarRange;

    int level;
    List<GameObject> cloneCars;
    List<GameObject> tempLevelStuff;
    List<GameObject> fakeCloneCars;
    int obstaclesUnlocked;
    bool canEveryoneMove;


    // Use this for initialization
    void Start() {
        obstaclesUnlocked = 2;
        tempLevelStuff = new List<GameObject>();
        cloneCars = new List<GameObject>();
        fakeCloneCars = new List<GameObject>();
        level = 2;

        StartLevel();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (!(GetComponent<AudioSource>().isPlaying))
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public void NextLevel() {
        foreach (GameObject item in tempLevelStuff)
        {
            Destroy(item);
        }
        foreach (GameObject item in cloneCars)
        {
            Destroy(item);
        }
        foreach (GameObject item in fakeCloneCars)
        {
            Destroy(item);
        }

        // pump up the difficulty
        level++;
        StartLevel();
    }

    public void TestPrint(string str) {
        Debug.Log(str);
    }

    void StartLevel()
    {
        canEveryoneMove = false;
        player.gameObject.GetComponent<ClownRules>().ResetMovementSpeed();
        tempLevelStuff = new List<GameObject>();
        cloneCars = new List<GameObject>();
        fakeCloneCars = new List<GameObject>();

        for (int i = 0; i < level; i++)
        {
            Vector3 location = new Vector3(cloneCarSpawnPoint, cloneCarRange - ((cloneCarRange * 2) / (level + 1)) * (i + 1));
            cloneCars.Add((GameObject)Instantiate(cloneCarsPrefab, location, Quaternion.identity));
        }

        int howManyObstacles = (int)(startingObstacleNumber * Mathf.Log(level, 2) * Mathf.Log(level, 2));
        for (int i = 0; i < howManyObstacles; i++)
        {
            GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclesUnlocked)];
            tempLevelStuff.Add((GameObject)Instantiate(obstacleToSpawn, new Vector3(Random.Range(-4.0f, cloneCarSpawnPoint - 1), Random.Range(cloneCarRange * -1, cloneCarRange)), Quaternion.identity));
        }

        for (int i = 0; i < level - 1; i++)
        {
            Vector3 location = new Vector3(-5, cloneCarRange - ((cloneCarRange * 2) / (level)) * (i + 1));
            GameObject thisCar = (GameObject)Instantiate(cloneCarsPrefab, location, Quaternion.identity);
            fakeCloneCars.Add(thisCar);
        }

        int whichClownAreYou = Random.Range(0, fakeCloneCars.Count * 4);
        foreach (GameObject car in fakeCloneCars)
        {
            spawn4Clowns(car.transform.position, whichClownAreYou);
            whichClownAreYou -= 4;
        }

        canEveryoneMove = true;
    }

    public List<GameObject> GetCloneCars()
    {
        return this.cloneCars;
    }

    void spawn4Clowns(Vector3 carPosition, int whichAreYou)
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject spawnedClown;
            if (i == whichAreYou)
            {
                player.ResetPosition(carPosition);
                spawnedClown = player.gameObject;
            }
            else
            {
                spawnedClown = (GameObject)Instantiate(otherClownsPrefab, carPosition, Quaternion.identity);
                tempLevelStuff.Add(spawnedClown);
            }
            float movementBuffer = 0;
            while (movementBuffer < 1.0f)
            {
                float toMove = Time.deltaTime;
                spawnedClown.transform.Translate(new Vector3(toMove, toMove * (((float)i - 1.5f) * 0.5f)));
                movementBuffer += toMove;
            }
        }
    }

    public bool everyoneCanMove()
    {
        return canEveryoneMove;
    }
}
