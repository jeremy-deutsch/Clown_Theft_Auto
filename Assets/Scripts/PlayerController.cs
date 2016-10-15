using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float normalMovementSpeed;
    public float slowMovementSpeed;
    public float slowMoveRecoveryTime;
    public GameController gameController;
    

    private float movementSpeed;
    private float slowMovementBuffer;

	// Use this for initialization
	void Start () {
        slowMovementBuffer = slowMoveRecoveryTime;
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        this.transform.Translate(new Vector3(horizontal, vertical, 0) * movementSpeed * Time.deltaTime);
        if (slowMovementBuffer < slowMoveRecoveryTime)
        {
            slowMovementBuffer += Time.deltaTime;
        }
        else
        {
            movementSpeed = normalMovementSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("clone car"))
        {
            gameController.NextLevel();
        }
        if (other.gameObject.CompareTag("slows you down"))
        {
            movementSpeed = slowMovementSpeed;
            slowMovementBuffer = 0;
        }
    }
}
