using UnityEngine;
using System.Collections;

public class OtherClownAI : MonoBehaviour {

    public float movementVariance;

    ClownRules cr;

    private float directionalTendency;
    private float speedTendency;

	// Use this for initialization
	void Start () {
        cr = this.GetComponent<ClownRules>();
        directionalTendency = Random.Range(movementVariance * -1, movementVariance);
        speedTendency = Random.Range(0.4f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {


        float xTranslation = Random.Range(speedTendency, 1.0f) * cr.GetMovementSpeed() * Time.deltaTime;
        float yTranslation = (directionalTendency * Random.Range(0, 1.0f) * cr.GetMovementSpeed()) * Time.deltaTime;
        transform.Translate(new Vector3(xTranslation, yTranslation));
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
