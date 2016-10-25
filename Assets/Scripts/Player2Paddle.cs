using UnityEngine;
using System.Collections;

public class Player2Paddle : MonoBehaviour
{
	private GameObject player2Paddle;

    public float MovementSpeed = 10f;

    private Vector2 playerPos = new Vector2(5.5f, -3.5f);

	void Start()
	{
		player2Paddle = GameObject.FindGameObjectWithTag ("Paddle1");
	}
    // Update is called once per frame
    void Update()
    {
        float newXPos = transform.position.x + (Input.GetAxis("Horizontal2") * MovementSpeed * Time.deltaTime);
		playerPos = new Vector2(Mathf.Clamp(newXPos, 2 + (player2Paddle.transform.localScale.x-1)/2, 9 - (player2Paddle.transform.localScale.x-1)/2), -3.5f);
        transform.position = playerPos;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball2")
        {
            GameObject.FindGameObjectWithTag("Game Manager").SendMessage("resetCombo", 2);
        }
    }
}
