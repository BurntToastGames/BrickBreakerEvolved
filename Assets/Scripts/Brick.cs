using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
    public float CollisionSpeedMult = 1.01f;

	void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(this.gameObject);

		col.rigidbody.AddForce(col.rigidbody.velocity * CollisionSpeedMult, ForceMode2D.Impulse);
    }
}
