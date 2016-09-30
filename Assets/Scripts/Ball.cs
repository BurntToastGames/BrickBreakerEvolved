using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	public float MinSpeed = 7.5f;
	public float MaxSpeed = 40f;
	public float StartingSpeed = 250f;
	private Vector2 startVelocity;



    private Rigidbody2D rig2D;

    private bool ballInPlay = false;
    private Vector2 paddleSpeed;

	// Use this for initialization
	void Start ()
    {
        rig2D = GetComponent<Rigidbody2D>();
	}

	void LateUpdate()
	{
		//Get ball up to minimum speed.
		if (rig2D.velocity.magnitude < MinSpeed && ballInPlay)
		{
			rig2D.velocity = Vector2.ClampMagnitude (rig2D.velocity * 10, MinSpeed);
		}

		if (rig2D.velocity.magnitude >= MaxSpeed) 
		{
			rig2D.velocity = Vector2.ClampMagnitude (rig2D.velocity, MaxSpeed);
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!ballInPlay && Input.GetButtonDown("Fire1"))
        {
            transform.parent = null;
            ballInPlay = true;
            rig2D.isKinematic = false;

            //Redirects Ball based on paddle velocity.
			startVelocity.Set(Input.GetAxis("Horizontal") * StartingSpeed, StartingSpeed);
            rig2D.AddForce(startVelocity);
        }

        outOfBoundsCheck();
	}

    void outOfBoundsCheck()
    {
        //Ball OOB + Reset
        if (transform.position.y < -4.5)
        {
            ballInPlay = false;
            transform.parent = GameObject.FindGameObjectWithTag("Paddle").transform;
            rig2D.isKinematic = true;
            rig2D.velocity = Vector2.zero;
            this.transform.position = new Vector2(transform.parent.position.x, transform.parent.position.y + 1.5f * transform.parent.localScale.y);
        }
    }
		
}
