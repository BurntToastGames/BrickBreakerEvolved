using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour
{
    public float MovementSpeed = 10f;

    public float PaddleCollisionMultX = 1.1f;

    private Vector2 playerPos = new Vector2(-5.5f, -3.5f);
	
	// Update is called once per frame
	void Update ()
    {
        float newXPos = transform.position.x + ( Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime);
        playerPos = new Vector2(Mathf.Clamp(newXPos, -9, -2), -3.5f);
        transform.position = playerPos;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
		if(col.gameObject.tag == "Ball")
		{
			float incomingSpeed = col.collider.attachedRigidbody.velocity.magnitude;
		}
	}
}
