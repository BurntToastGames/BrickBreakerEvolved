using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D col)
    {
        GameObject brickBlob = this.transform.parent.transform.parent.gameObject;

        if(brickBlob.tag == "Bricks1")
        {
            GameObject.FindGameObjectWithTag("Game Manager").SendMessage("sendBricks", 1);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Game Manager").SendMessage("sendBricks", 2);
        }

        Destroy(this.gameObject);
    }
}
