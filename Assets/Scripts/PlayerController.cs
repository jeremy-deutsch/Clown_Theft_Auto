using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    
    public GameController gameController;

    ClownRules cr;

    private Animator animator;

	// Use this for initialization
	void Start () {
        cr = this.GetComponent<ClownRules>();
        this.animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameController.everyoneCanMove())
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (horizontal == 0 && vertical == 0)
            {
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isRunning", true);
            }
            this.transform.Translate(new Vector3(horizontal, vertical, 0) * cr.GetMovementSpeed() * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        
        if (other.gameObject.CompareTag("clone car"))
        {
            Debug.Log("reached the clone car");
            gameController.NextLevel();
        }
    }

    public void ResetPosition (Vector3 placeToGo)
    {
        this.transform.position = placeToGo;//new Vector3(-5.0f, 0);
        cr.ResetMovementSpeed();
    }
    
}
