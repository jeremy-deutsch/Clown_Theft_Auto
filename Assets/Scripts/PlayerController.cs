using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    
    public GameController gameController;

    ClownRules cr;

	// Use this for initialization
	void Start () {
        cr = this.GetComponent<ClownRules>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        this.transform.Translate(new Vector3(horizontal, vertical, 0) * cr.GetMovementSpeed() * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        
        if (other.gameObject.CompareTag("clone car"))
        {
            Debug.Log("reached the clone car");
            gameController.NextLevel();
        }
    }
    
}
