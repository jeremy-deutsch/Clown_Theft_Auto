using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public PlayerController player;
    public GameObject cloneCars;
    public GameObject otherClownsPrefab;
    public GameObject[] obstaclePrefabs;
    public int howManyObstacles;

    int level;
    List<GameObject> tempLevelStuff;
    int obstaclesUnlocked;
    

	// Use this for initialization
	void Start () {
        obstaclesUnlocked = 2;
        tempLevelStuff = new List<GameObject>();
        level = 0;

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
        tempLevelStuff = new List<GameObject>();

        // pump up the difficulty

        StartLevel();
    }

    public void TestPrint(string str) {
        Debug.Log(str);
    }

    void StartLevel ()
    {
        for (int i = 0; i < howManyObstacles; i++)
        {
            GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclesUnlocked)];
            tempLevelStuff.Add((GameObject)Instantiate(obstacleToSpawn, new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f)), Quaternion.identity));
        }

        player.transform.position = new Vector3(-5.0f, 0);
        tempLevelStuff.Add((GameObject)Instantiate(otherClownsPrefab, new Vector3(-5.0f, 1), Quaternion.identity));
        tempLevelStuff.Add((GameObject)Instantiate(otherClownsPrefab, new Vector3(-5.0f, 2), Quaternion.identity));
        tempLevelStuff.Add((GameObject)Instantiate(otherClownsPrefab, new Vector3(-5.0f, -1), Quaternion.identity));
        tempLevelStuff.Add((GameObject)Instantiate(otherClownsPrefab, new Vector3(-5.0f, -2), Quaternion.identity));

    }
}
