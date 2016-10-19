using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Player player1, player2;
    public GameObject LinePrefab;
	public GameObject gameOverPanel;
	public int maxLineCount;

    public int bricksPerLine = 12;    //bricks needed to send a line.
    public float scorePerBrick = 50f; //score needed to send a brick.
    public float brickValue = 100f;    //default score awarded per brick hit.

    private float lineSpaceConst = 1.14f;

	private float p1AddLineYOffset = 0;
	private float p2AddLineYOffset = 12;

	private Text player1PendingText;
	private Text player2PendingText;

    private Text player1ScoreText;
    private Text player2ScoreText;

    private Text player1WinsText;
    private Text player2WinsText;

    private Text gameOverText;

    // Use this for initialization
    void Start ()
    {
		Time.timeScale = 1f;
		player1PendingText = GameObject.Find("Player 1 Pending").GetComponent<Text>();
		player2PendingText = GameObject.Find("Player 2 Pending").GetComponent<Text>();

        player1ScoreText = GameObject.Find("Player 1 Score").GetComponent<Text>();
        player2ScoreText = GameObject.Find("Player 2 Score").GetComponent<Text>();

        player1WinsText = GameObject.Find("Player 1 Wins").GetComponent<Text>();
        player2WinsText = GameObject.Find("Player 2 Wins").GetComponent<Text>();

		gameOverText = GameObject.Find("OutcomeText").GetComponent<Text>();

		gameOverPanel.SetActive (false);

        
        player1 = new Player()
        {
            wins = 0,

            score = 0,
            comboCount = 0,
			name = "Player 1",

            brickCount = brickCountHelper(GameObject.FindGameObjectWithTag("Bricks1")),
            pendingBricks = 0,

            BrickGroup = GameObject.FindGameObjectWithTag("Bricks1"),
            Paddle = GameObject.FindGameObjectWithTag("Paddle1"),
            Ball = GameObject.FindGameObjectWithTag("Ball1"),

			recentlyAddedLineY = p1AddLineYOffset
        };

        player2 = new Player()
        {
            wins = 0,

            score = 0,
            comboCount = 0,
			name = "Player 2",

            brickCount = brickCountHelper(GameObject.FindGameObjectWithTag("Bricks2")),
            pendingBricks = 0,

            BrickGroup = GameObject.FindGameObjectWithTag("Bricks2"),
            Paddle = GameObject.FindGameObjectWithTag("Paddle2"),
            Ball = GameObject.FindGameObjectWithTag("Ball2"),

			recentlyAddedLineY = p2AddLineYOffset
        };

        player1WinsText.text = player1.wins.ToString();
        player2WinsText.text = player2.wins.ToString();
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
        //Victory by board clear.
        checkClearVictory(player1, player2);
    }

	//End the game, display the GameOver panel, stop time, and display outcome text
	void gameOver(Player winner, Player loser)
	{
		print ("GameOver");		
		gameOverPanel.SetActive(true);
        gameOverText.text = winner.name + " wins!";

        winner.wins++;
        player1WinsText.text = winner.wins.ToString();
        player2WinsText.text = loser.wins.ToString();
		
        Time.timeScale = 0f;
	}
	
    //Check who has won the game based on number of lines in each player's screen (more conditions to be added)
	void checkClearVictory(Player player1, Player player2)
	{
		if (player1.BrickGroup.transform.childCount <= 0) {
			gameOver(player1, player2);
		}
		if (player2.BrickGroup.transform.childCount <= 0) {
			gameOver(player2, player1);
		}
	}
	void checkLineVictory(Player player1, Player player2)
	{
		//print ("Player1BrickLines :" + player1.BrickGroup.transform.childCount);
		print ("Player2BrickLines :" + player2.BrickGroup.transform.childCount);

		if (player1.BrickGroup.transform.childCount >= maxLineCount) {
			gameOver(player2, player1);
		}
		if (player2.BrickGroup.transform.childCount >= maxLineCount) {
			gameOver(player1, player2);
		}
	}
    
    // *Messenger Method*
    // Sends bricks to the opponent of 'player' based on the player's current combo. Manages score too.
    void sendBricks(int player)
    {
        Player tempPlayer = player == 1 ? player1 : player2;
        Player victim = player == 2 ? player1 : player2;

        AwardScore(tempPlayer, victim);

        //Minor bug on adding more than bricksPerLine to pendingBricks.
		while (tempPlayer.pendingBricks >= bricksPerLine)
		{
			AddLine (tempPlayer, victim);
		}

		player1PendingText.text = "Pending : " + player2.pendingBricks;
		player2PendingText.text = "Pending : " + player1.pendingBricks;

        player1ScoreText.text = "Score : " + player1.score;
        player2ScoreText.text = "Score : " + player2.score;


        //Victory by Line #
		checkLineVictory(player1, player2);
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
    void AwardScore(Player tempPlayer ,Player tempVictim)
    {
        //Calculate and award score , increment combo count.
        float brickScore = brickValue + (brickValue * tempPlayer.comboCount) / 10;
        tempPlayer.score += brickScore;
        tempPlayer.comboCount++;

        int bricksToSend = (int)(brickScore / scorePerBrick);

        if (tempVictim.pendingBricks > 0)
        {
            int initialVictimPendingBricks = tempVictim.pendingBricks;

            tempVictim.pendingBricks = (tempVictim.pendingBricks - bricksToSend) < 0 ? 0 : tempVictim.pendingBricks - bricksToSend;
            bricksToSend = bricksToSend - initialVictimPendingBricks;
            tempPlayer.pendingBricks = bricksToSend > 0 ? tempPlayer.pendingBricks + bricksToSend : tempPlayer.pendingBricks;
        }
        else
        {
            tempPlayer.pendingBricks += (int)(brickScore / scorePerBrick);
        }

        //print((int)(brickScore / scorePerBrick) + " pending bricks added");

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
    public int wins { get; set; }

    public float score { get; set; }
    public int comboCount { get; set; }

    public int brickCount { get; set; }                     

    public int pendingBricks { get; set; }  // Bricks player will send to victim.

    public GameObject BrickGroup { get; set; }
    public GameObject Paddle { get; set; }
    public GameObject Ball { get; set; }

    internal float recentlyAddedLineY;

	public string name { get; set; }

    public Player()
    { }
}