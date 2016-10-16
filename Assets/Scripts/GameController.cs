using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public PlayerController player;
    public GameObject otherClownsPrefab;
    public GameObject[] obstaclePrefabs;
    public int startingObstacleNumber;
    public GameObject cloneCarsPrefab;
    public float cloneCarSpawnPoint;

    int level;
    List<GameObject> cloneCars;
    List<GameObject> tempLevelStuff;
    int obstaclesUnlocked;
    

	// Use this for initialization
	void Start () {
        obstaclesUnlocked = 2;
        tempLevelStuff = new List<GameObject>();
        cloneCars = new List<GameObject>();
        level = 2;

        StartLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NextLevel () {
        foreach (GameObject item in tempLevelStuff)
        {
            Destroy(item);
        }
        foreach (GameObject item in cloneCars)
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

    void StartLevel ()
    {

        player.gameObject.GetComponent<ClownRules>().ResetMovementSpeed();
        tempLevelStuff = new List<GameObject>();
        cloneCars = new List<GameObject>();

        for (int i = 0; i < level; i++)
        {
            Vector3 location = new Vector3(cloneCarSpawnPoint, 4.0f - (8.0f / (level + 1)) * (i + 1));
            cloneCars.Add((GameObject)Instantiate(cloneCarsPrefab, location, Quaternion.identity));
        }

        int howManyObstacles = (int)(startingObstacleNumber * Mathf.Log(level, 2));
        for (int i = 0; i < howManyObstacles; i++)
        {
            GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclesUnlocked)];
            tempLevelStuff.Add((GameObject)Instantiate(obstacleToSpawn, new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f)), Quaternion.identity));
        }

        player.ResetPosition();
        tempLevelStuff.Add((GameObject)Instantiate(otherClownsPrefab, new Vector3(-5.0f, 1), Quaternion.identity));
        tempLevelStuff.Add((GameObject)Instantiate(otherClownsPrefab, new Vector3(-5.0f, 2), Quaternion.identity));
        tempLevelStuff.Add((GameObject)Instantiate(otherClownsPrefab, new Vector3(-5.0f, -1), Quaternion.identity));
        tempLevelStuff.Add((GameObject)Instantiate(otherClownsPrefab, new Vector3(-5.0f, -2), Quaternion.identity));

    }

    public List<GameObject> GetCloneCars ()
    {
        return this.cloneCars;
    }
}
