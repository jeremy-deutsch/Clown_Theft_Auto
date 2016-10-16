using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OtherClownAI : MonoBehaviour {

    public float movementVariance;
    public GameController gameController;

    ClownRules cr;

    private float directionalTendency;
    private float speedTendency;
    private float targetedCarHeight;

	// Use this for initialization
	void Start () {
        cr = this.GetComponent<ClownRules>();
        directionalTendency = Random.Range(movementVariance * -1, movementVariance);
        speedTendency = Random.Range(0.4f, 1.0f);
        GameObject thing = GameObject.FindWithTag("GameController");
        if(thing != null)
        {
            Debug.Log("We found a GameController!");
            gameController = thing.GetComponent<GameController>();

        }
        targetedCarHeight = targetACar();
        Debug.Log("My target height is " + targetedCarHeight + "!");
    }
	
	// Update is called once per frame
	void Update () {
        if (gameController.everyoneCanMove())
        {

            float xTranslation = Random.Range(speedTendency, 1.0f) * cr.GetMovementSpeed() * Time.deltaTime;
            float carTendency;
            if (this.transform.position.y > targetedCarHeight)
            {
                carTendency = Random.Range(0.5f, 1.0f);
            }
            else
            {
                carTendency = Random.Range(-1.0f, -0.5f);
            }
            float yTranslation = (directionalTendency * Random.Range(0, 1.0f) + carTendency) * cr.GetMovementSpeed() * Time.deltaTime;
            transform.Translate(new Vector3(xTranslation, yTranslation));
        }
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("clone car"))
        {
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    float targetACar()
    {
        List<GameObject> cars = gameController.GetCloneCars();
        float targetedArea = transform.position.y + Random.Range(-0.2f, 0.2f);
        float lowBall = -1000.0f;
        float highBall = 1000.0f;
        foreach (GameObject car in gameController.GetCloneCars())
        {
            if (car.transform.position.y < targetedArea)
            {
                lowBall = car.transform.position.y;
                break;
            }
            else
            {
                highBall = car.transform.position.y;
            }
        }
        if (highBall - targetedArea < targetedArea - lowBall)
        {
            return highBall;
        }
        else
        {
            return lowBall;
        }
    }
}
