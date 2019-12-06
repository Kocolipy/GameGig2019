using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject tower;
    public GameObject player1;
    public GameObject player2;
    public GameObject HUD;

    private bool inPlay = true;
    private Text resultText;

    // Game Timer
    private int gameTime = 100;
    private Text gameTimeText;

    // Towers
    private GameObject[] towers = new GameObject[2];
    
    // Scores
    Dictionary<string, int> score = new Dictionary<string, int>();
    private Text p1score;
    private Text p2score;

    // Start is called before the first frame update
    void Start()
    {
        gameTimeText = HUD.transform.Find("GameTimer").GetComponent<Text>();
        resultText = HUD.transform.Find("Results").GetComponent<Text>();

        // Initialising players
        player1 = Instantiate(player1, new Vector3(-6, -0.9f, -0.1f), Quaternion.identity);
        player2 = Instantiate(player2, new Vector3( 6, -0.9f, -0.1f), Quaternion.identity);

        // Initialising towers
        towers[0] = Instantiate(tower, new Vector3(0, -0.4f, 0), Quaternion.identity);
        towers[1] = Instantiate(tower, new Vector3(-5f, -4.3f, 0), Quaternion.identity);
        towers[1] = Instantiate(tower, new Vector3(5f, -4.3f, 0), Quaternion.identity);
        towers[1] = Instantiate(tower, new Vector3(-5f, 3.6f, 0), Quaternion.identity);
        towers[1] = Instantiate(tower, new Vector3(5f, 3.6f, 0), Quaternion.identity);

        // Initialising Scores
        score[player1.name] = 0;
        score[player2.name] = 0;
        p1score = HUD.transform.Find("Score1").GetComponent<Text>();
        p2score = HUD.transform.Find("Score2").GetComponent<Text>();

        // Update points every second
        InvokeRepeating("UpdateSec", 0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateSec()
    {
        if (inPlay)
        {
            for (int i = 0; i < towers.Length; i++)
            {
                if (towers[i].GetComponent<Tower>().getOwner() != null)
                {
                    score[towers[i].GetComponent<Tower>().getOwner().name] += 1;
                }
            }

            p1score.text = score[player1.name].ToString();
            p2score.text = score[player2.name].ToString();

            gameTime -= 1;
            gameTimeText.text = gameTime.ToString();
            if (gameTime == 0)
            {
                gameOver(score);
            }
        }
    }

    void gameOver(Dictionary<string, int> score)
    {
        inPlay = false;

        // Decide winner
        string winner;
        if (score[player1.name] == score[player2.name]){
            winner = "Draw!";
        } else{
            winner = score[player1.name] > score[player2.name] ? "1" : "2";
            winner = "Player " + winner + " wins!";
        }
        gameTimeText.text = "";
        resultText.text = winner;

        // Clear Game
        Destroy(player1);
        Destroy(player2);
        for (int i = 0; i < towers.Length; i++)
        {
            Destroy(towers[i]);
        }

    }
}
