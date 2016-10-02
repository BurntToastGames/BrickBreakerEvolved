using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Player player1, player2;

    public GameObject LinePrefab;

    private int bricksPerLine = 12;
    private float lineSpaceConst = 1.14f;

    

	// Use this for initialization
	void Start ()
    {
        player1 = new Player()
        {
            BrickGroup = GameObject.FindGameObjectWithTag("Bricks1"),
            brickCount = brickCountHelper(GameObject.FindGameObjectWithTag("Bricks1")),
            pendingBricks = 0,
            Paddle = GameObject.FindGameObjectWithTag("Paddle1"),
            Ball = GameObject.FindGameObjectWithTag("Ball1"),
            recentlyAddedLineY = 0
        };

        player2 = new Player()
        {
            BrickGroup = GameObject.FindGameObjectWithTag("Bricks2"),
            brickCount = brickCountHelper(GameObject.FindGameObjectWithTag("Bricks2")),
            pendingBricks = 0,
            Paddle = GameObject.FindGameObjectWithTag("Paddle2"),
            Ball = GameObject.FindGameObjectWithTag("Ball2"),
            recentlyAddedLineY = 0
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
        Player tempPlayer = player == 1 ? player1 : player2;
        Player victim = player == 2 ? player1 : player2;

        tempPlayer.pendingBricks++;
        tempPlayer.brickCount--;

        if(tempPlayer.pendingBricks == bricksPerLine)
        {
            tempPlayer.pendingBricks -= bricksPerLine;

            Vector3 newBrickGroupPosition = new Vector3(victim.BrickGroup.transform.position.x,
                                                        victim.BrickGroup.transform.position.y - (lineSpaceConst * victim.BrickGroup.transform.localScale.y));

            victim.BrickGroup.transform.position = newBrickGroupPosition;

            Vector3 newLinePositionWithinParent = new Vector3(0, victim.recentlyAddedLineY + lineSpaceConst);
            victim.recentlyAddedLineY += lineSpaceConst;

            GameObject newLine = Instantiate(LinePrefab, victim.BrickGroup.transform.position, Quaternion.identity) as GameObject;
            newLine.transform.parent = victim.BrickGroup.transform;
            newLine.transform.localPosition = newLinePositionWithinParent;
            newLine.transform.localScale = Vector3.one;
        }

        print("Player" + player + " : " + tempPlayer.pendingBricks + " pending");
    }
}


public class Player
{
    public int brickCount { get; set; }                     

    public int pendingBricks { get; set; }

    public GameObject BrickGroup { get; set; }
    public GameObject Paddle { get; set; }
    public GameObject Ball { get; set; }

    internal float recentlyAddedLineY;

    public Player()
    { }
}