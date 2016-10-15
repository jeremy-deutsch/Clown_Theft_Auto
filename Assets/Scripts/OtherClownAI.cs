using UnityEngine;
using System.Collections;

public class OtherClownAI : MonoBehaviour {

    public float movementVariance;

    ClownRules cr;

    private float tendency;

	// Use this for initialization
	void Start () {
        cr = this.GetComponent<ClownRules>();
        tendency = Random.Range(movementVariance * -1, movementVariance);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(Random.Range(0.7f, 1.0f), tendency * Random.Range(0, 1.0f)) * cr.GetMovementSpeed() * Time.deltaTime);
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("clone car"))
        {
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
