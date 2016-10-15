using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NextLevel () {
        Debug.Log("Ran GameController.NextLevel()");
    }

    public void TestPrint(string str) {
        Debug.Log(str);
    }
}
