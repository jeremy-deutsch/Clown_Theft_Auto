using UnityEngine;
using System.Collections;

public class OtherClownAI : MonoBehaviour {

    public float movementVariance;

    private float tendency;

	// Use this for initialization
	void Start () {
        tendency = Random.Range(movementVariance * -1, movementVariance);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
