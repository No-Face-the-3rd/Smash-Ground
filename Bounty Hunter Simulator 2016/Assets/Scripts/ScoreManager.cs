using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scorer;

    public Text[] scoreTexts;
    private int[] scores;
    private float pointLossTimer;
    public float pointLossInterval;
    public int pointLossPerInterval;

    void Awake()
    {
        if (scorer == null)
        {
            DontDestroyOnLoad(gameObject);
            scorer = this;
        }
        else if(scorer != this)
        {
            Destroy(gameObject);
        }
    }

	void Start ()
    {
        scores = new int[2];
        pointLossTimer = 0.0f;
	}

    void Update()
    {
        for (int i = 0; i < PlayerLocator.locator.players.Length; i++)
        {
            int idTmp = PlayerLocator.locator.players[i].GetComponent<PlayerController>().playerNum - 1;
            if (idTmp < scoreTexts.Length)
            {
                scoreTexts[idTmp].text = "Hotdogs: " + scores[idTmp];
            }
            if (pointLossTimer >= pointLossInterval)
            {
                pointLossTimer = 0.0f;
                PlayerController player = PlayerLocator.locator.players[i].GetComponent<PlayerController>();
                if (player.child != null || player.nextRoom.Count > 0 || (player.curRoom.Count > 0 && PlayerLocator.locator.hasSpawned[i]))
                {
                    scores[idTmp] -= pointLossPerInterval;
                }
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
        for(int i = 0;i < PlayerLocator.locator.players.Length;i++)
        {
            if(PlayerLocator.locator.players[i].GetComponent<PlayerController>().playerNum == player)
            {
                if (PlayerLocator.locator.players[i].GetComponent<PlayerController>().powerup == PlayerController.powerUps.HOTDOG)
                    score += score;
                if (PlayerLocator.locator.players[i].GetComponent<PlayerController>().child != null && PlayerLocator.locator.players[i].GetComponent<PlayerController>().child.arrayIndex == 1)
                    score += score / 2;
                break;
            }
        }
        scores[player - 1] += score;
    }
}
