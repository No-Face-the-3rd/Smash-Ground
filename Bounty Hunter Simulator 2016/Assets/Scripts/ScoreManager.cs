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

    void Update()
    {
        for (int i = 0; i < locator.players.Length; i++)
        {
            int idTmp = locator.players[i].GetComponent<PlayerController>().playerNum - 1;
            if (idTmp < scoreTexts.Length)
            {
                scoreTexts[idTmp].text = "Hotdogs: " + scores[idTmp];
            }
            if (pointLossTimer >= pointLossInterval)
            {
                pointLossTimer = 0.0f;
                PlayerController player = locator.players[i].GetComponent<PlayerController>();
                if (player.child != null || player.nextRoom.Count > 0 || (player.curRoom.Count > 0 && locator.hasSpawned[i])
                    scores[idTmp] -= pointLossPerInterval;
                if (scores[idTmp] < 0)
                {
                    scores[idTmp] = 0;
                }
            }
        }
    
        pointLossTimer += Time.deltaTime;
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
