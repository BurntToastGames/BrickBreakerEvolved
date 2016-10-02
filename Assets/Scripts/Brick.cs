using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D col)
    {
        //Lets GM know when a brick has been broken.
        GMSendBricks();

        //Deletes a line prefab if all its bricks have been broken.
        LineCleanUp();

        Destroy(this.gameObject);
    }

    void GMSendBricks()
    {
        GameObject brickBlob = this.transform.parent.transform.parent.gameObject;

        if (brickBlob.tag == "Bricks1")
        {
            GameObject.FindGameObjectWithTag("Game Manager").SendMessage("sendBricks", 1);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Game Manager").SendMessage("sendBricks", 2);
        }
    }

    void LineCleanUp()
    {
        if (this.transform.parent.childCount == 1)
        {
            Destroy(this.transform.parent.gameObject);
            this.transform.parent = null;
        }
    }

}
