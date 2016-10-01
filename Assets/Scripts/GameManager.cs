using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Player player1, player2;

	// Use this for initialization
	void Start ()
    {
        player1 = new Player()
        {
            brickCount = brickCountHelper(GameObject.FindGameObjectWithTag("Bricks1")),
            pendingBricks = 0,
            Paddle = GameObject.FindGameObjectWithTag("Paddle1"),
            Ball = GameObject.FindGameObjectWithTag("Ball1")
        };

        player2 = new Player()
        {
            brickCount = brickCountHelper(GameObject.FindGameObjectWithTag("Bricks2")),
            pendingBricks = 0,
            Paddle = GameObject.FindGameObjectWithTag("Paddle2"),
            Ball = GameObject.FindGameObjectWithTag("Ball2")
        };
    }

    int brickCountHelper(GameObject brickGroup)
    {
        int brickCount = 0;
        for (int i = 0; i < brickGroup.transform.childCount; i++)
        {
            brickCount += brickGroup.transform.GetChild(i).childCount;
        }

        return brickCount;
    }


    // Update is called once per frame
    void Update ()
    {

	}

    void sendBricks(int player)
    {
        Player temp = player == 1 ? player1 : player2;

        temp.pendingBricks++;
        temp.brickCount--;

        print("Player" + player + " : " + temp.pendingBricks + " pending");
    }

}


public class Player
{
    public int brickCount { get; set; }                     

    public int pendingBricks { get; set; }

    public GameObject Paddle { get; set; }
    public GameObject Ball { get; set; }

    public Player()
    { }
}