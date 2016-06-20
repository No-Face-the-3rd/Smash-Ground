using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text[] scoreTexts;
    private PlayerLocator locator;
    private int[] scores;
    private float pointLossTimer;
    public float pointLossInterval;
    public int pointLossPerInterval;

	void Start ()
    {
        locator = FindObjectOfType<PlayerLocator>();
        scores = new int[2];
        pointLossTimer = 0.0f;
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
        pointLossTimer += Time.deltaTime;
        if(pointLossTimer >= pointLossInterval)
        {
            pointLossTimer = 0.0f;
            for(int i = 0;i < scores.Length;i++)
            {
                scores[i] -= pointLossPerInterval;
                if(scores[i] < 0)
                {
                    scores[i] = 0;
                }
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
                if (locator.players[i].GetComponent<PlayerController>().child != null && locator.players[i].GetComponent<PlayerController>().child.arrayIndex == 1)
                    score += score / 2;
                break;
            }
        }
        scores[player - 1] += score;
    }
}
