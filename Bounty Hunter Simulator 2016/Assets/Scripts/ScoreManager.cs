using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text[] scoreTexts;
    private PlayerLocator locator;
    private int[] scores;

	void Start ()
    {
        locator = FindObjectOfType<PlayerLocator>();
        scores = new int[2];
	}
	
	void Update ()
    {
        for (int i = 0; i < locator.players.Length; i++)
        {
            int idTmp = locator.players[i].GetComponent<PlayerController>().playerNum - 1;
            if (idTmp < scoreTexts.Length)
            {
                scoreTexts[idTmp].text = "Hotdogs: " + scores[idTmp];
            }
        }
    }

    public void addScore(int player,int score)
    {
        for(int i = 0;i < locator.players.Length;i++)
        {
            if(locator.players[i].GetComponent<PlayerController>().playerNum == player)
            {
                if (locator.players[i].GetComponent<PlayerController>().powerup == PlayerController.powerUps.HOTDOG)
                    score += score;
                break;
            }
        }
        scores[player - 1] += score;
    }
}
