using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(this.gameObject);
    }
}
