using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Player player1, player2;

    public GameObject LinePrefab;

    private int bricksPerLine = 12;    //bricks needed to send a line.
    private float scorePerBrick = 50f; //score needed to send a brick.
    private float brickValue = 100f;    //default score awarded per brick hit.

    private float lineSpaceConst = 1.14f;

	private float p1AddLineYOffset = 0;
	private float p2AddLineYOffset = 12;

	private Text player1PendingText;
	private Text player2PendingText;

    

	// Use this for initialization
	void Start ()
    {
		player1PendingText = GameObject.Find("Player 1 Pending").GetComponent<Text>();
		player2PendingText = GameObject.Find("Player 2 Pending").GetComponent<Text>();

        player1 = new Player()
        {
            score = 0,
            comboCount = 0,

            brickCount = brickCountHelper(GameObject.FindGameObjectWithTag("Bricks1")),
            pendingBricks = 0,

            BrickGroup = GameObject.FindGameObjectWithTag("Bricks1"),
            Paddle = GameObject.FindGameObjectWithTag("Paddle1"),
            Ball = GameObject.FindGameObjectWithTag("Ball1"),

			recentlyAddedLineY = p1AddLineYOffset
        };

        player2 = new Player()
        {
            score = 0,
            comboCount = 0,

            brickCount = brickCountHelper(GameObject.FindGameObjectWithTag("Bricks2")),
            pendingBricks = 0,

            BrickGroup = GameObject.FindGameObjectWithTag("Bricks2"),
            Paddle = GameObject.FindGameObjectWithTag("Paddle2"),
            Ball = GameObject.FindGameObjectWithTag("Ball2"),

			recentlyAddedLineY = p2AddLineYOffset
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

    // *Messenger Method*
    // Sends bricks to the opponent of 'player' based on the player's current combo. Manages score too.
    void sendBricks(int player)
    {
        Player tempPlayer = player == 1 ? player1 : player2;
        Player victim = player == 2 ? player1 : player2;

        AwardScore(tempPlayer);

		if (tempPlayer.pendingBricks >= bricksPerLine)
		{
			AddLine (tempPlayer, victim);
		}

		player1PendingText.text = "Pending : " + player2.pendingBricks;
		player2PendingText.text = "Pending : " + player1.pendingBricks;
    }

    // *Messenger Method*
    // Resets combo of a player.
    void resetCombo(int player)
    {
        Player tempPlayer = player == 1 ? player1 : player2;

        tempPlayer.comboCount = 0;
    }

    // Awards Score to a player upon breaking a brick.
    // Adds appropriate amount of bricks based on score.
    void AwardScore(Player tempPlayer)
    {
        float brickScore = brickValue + (brickValue * tempPlayer.comboCount) / 10;
        tempPlayer.score += brickScore;
        tempPlayer.comboCount++;

        tempPlayer.pendingBricks += (int)(brickScore / scorePerBrick);
        print((int)(brickScore / scorePerBrick) + " pending bricks added");

        tempPlayer.brickCount--;
    }

	void AddLine(Player tempPlayer , Player victim)
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
}



public class Player
{
    public float score { get; set; }
    public int comboCount { get; set; }

    public int brickCount { get; set; }                     

    public int pendingBricks { get; set; }  // Bricks player will send to victim.

    public GameObject BrickGroup { get; set; }
    public GameObject Paddle { get; set; }
    public GameObject Ball { get; set; }

    internal float recentlyAddedLineY;

    public Player()
    { }
}