using UnityEngine;
using System.Collections;

public class Player1Paddle : MonoBehaviour
{
	private GameObject player1Paddle;

    public float MovementSpeed = 10f;

    private Vector2 playerPos = new Vector2(-5.5f, -3.5f);

	void Start()
	{
		player1Paddle = GameObject.FindGameObjectWithTag ("Paddle1");
	}
	// Update is called once per frame
	void Update ()
    {
        float newXPos = transform.position.x + ( Input.GetAxis("Horizontal1") * MovementSpeed * Time.deltaTime);
		playerPos = new Vector2(Mathf.Clamp(newXPos, -9 + (player1Paddle.transform.localScale.x-1)/2, -2 - (player1Paddle.transform.localScale.x-1)/2), -3.5f); //Clamps correctly based on any X localScale of paddle
        transform.position = playerPos;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ball1")
        {
            GameObject.FindGameObjectWithTag("Game Manager").SendMessage("resetCombo", 1);
        }
    }

}
