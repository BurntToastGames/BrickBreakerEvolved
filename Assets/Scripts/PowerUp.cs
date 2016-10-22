using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    public GameObject PowerUpPaddle;

    public float FallSpeed = 5f;

    Transform trans;

	// Use this for initialization
	void Start ()
    {
        trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float newYPos = transform.position.y - (FallSpeed * Time.deltaTime);
        trans.position = new Vector2(trans.position.x, newYPos);

        if(trans.position.y < -4.5)
        {
            Destroy(this);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print(col.gameObject.name);
        if(col.gameObject == PowerUpPaddle)
        {
            print("gottem");
            GameObject.Find("Game Manager").SendMessage("applyPowerUp", new applyPowerUpInfo { player = col.gameObject.tag.EndsWith("1") ? 1 : 2 ,
                                                                                               powerUpKey = PowerUpKey.Test} );
            Destroy(this.gameObject);
        }
    }
}

public struct applyPowerUpInfo
{
    public int player { get; set; }

    public PowerUpKey powerUpKey { get; set; }
}

public enum PowerUpKey
{
    Test
}