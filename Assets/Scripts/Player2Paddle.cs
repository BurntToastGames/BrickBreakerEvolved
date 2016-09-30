using UnityEngine;
using System.Collections;

public class Player2Paddle : MonoBehaviour
{

    public float MovementSpeed = 10f;

    private Vector2 playerPos = new Vector2(5.5f, -3.5f);

    // Update is called once per frame
    void Update()
    {
        float newXPos = transform.position.x + (Input.GetAxis("Horizontal2") * MovementSpeed * Time.deltaTime);
        playerPos = new Vector2(Mathf.Clamp(newXPos, 2, 9), -3.5f);
        transform.position = playerPos;
    }
}
