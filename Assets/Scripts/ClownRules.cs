using UnityEngine;
using System.Collections;

public class ClownRules : MonoBehaviour {

    public float normalMovementSpeed;
    public float slowMovementSpeed;
    public float slowMoveRecoveryTime;
    public float freezeRecoveryTime;
    public GameController gameController;

    float movementSpeed;
    private float slowMovementBuffer;
    private float frozenBuffer;

    // Use this for initialization
    void Start () {
        slowMovementBuffer = slowMoveRecoveryTime;
        frozenBuffer = freezeRecoveryTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (frozenBuffer < freezeRecoveryTime) // if you're frozen, increment toward becoming unfrozen
        {
            movementSpeed = 0;
            frozenBuffer += Time.deltaTime;
        }
        else if (slowMovementBuffer < slowMoveRecoveryTime) // slowdown recovery pauses when you're frozen
        {
            movementSpeed = slowMovementSpeed;
            slowMovementBuffer += Time.deltaTime;
        }
        else
        {
            movementSpeed = normalMovementSpeed; // if you're no longer frozen or slow, move normally
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("freezes you"))
        {
            movementSpeed = 0;
            frozenBuffer = 0;
            Destroy(other.gameObject, freezeRecoveryTime / 2);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("slows you down"))
        {
            movementSpeed = slowMovementSpeed;
            slowMovementBuffer = 0;
        }
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public void ResetMovementSpeed ()
    {
        frozenBuffer = freezeRecoveryTime;
        slowMovementBuffer = slowMoveRecoveryTime;
        movementSpeed = normalMovementSpeed;
    }
}
