using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
	public float nudgeForce = 100f;

	void OnCollisionStay2D(Collision2D col)
	{
		print ("Triggered in 2016");

		if(transform.position.x > col.rigidbody.position.x)
		{
			col.rigidbody.AddForce(new Vector2(-nudgeForce, 0));
		}
		else
		{
			col.rigidbody.AddForce(new Vector2(nudgeForce, 0));
		}

	}
}
