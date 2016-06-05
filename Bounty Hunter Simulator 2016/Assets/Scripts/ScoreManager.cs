using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text[] scoreTexts;
    private PlayerLocator locator;
    private GameObject [] players;

    private int[] scores;
	void Start ()
    {
        locator = FindObjectOfType<PlayerLocator>();
        scores = new int[2];
	}
	
	void Update ()
    {
	    players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < /*locator.*/players.Length; i++)
        {
            int idTmp = /*locator.*/players[i].GetComponent<PlayerController>().playerNum - 1;
            if (idTmp < scoreTexts.Length)
            {
                scoreTexts[idTmp].text = "Success: " + scores[idTmp];
            }
        }
    }

    public void addScore(int player,int score)
    {
        scores[player - 1] += score;
    }
}
