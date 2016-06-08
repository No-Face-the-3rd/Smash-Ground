using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILifeManagerScript : MonoBehaviour
{
    public Text[] liveTexts;
    private PlayerLocator locator;
    private bool curRoom;
    private float swapRoom;

	void Start ()
    {
        swapRoom = 0.0f;
        curRoom = true;
       locator = FindObjectOfType<PlayerLocator>();
	}
	
	void Update ()
    {
        for(int i =0;i < locator.players.Length;i++)
        {
            int idTmp = locator.players[i].GetComponent<PlayerController>().playerNum - 1;
            if (idTmp < liveTexts.Length)
            {
                if (curRoom)
                {
                    liveTexts[idTmp].text = "Current: " + locator.players[i].GetComponent<PlayerController>().curRoom.Count;
                }
                else
                {
                    liveTexts[idTmp].text = "Next: " + locator.players[i].GetComponent<PlayerController>().nextRoom.Count;
                }
            }
        }
        if(swapRoom >= 2.0f)
        {
            curRoom = !curRoom;
            swapRoom = 0.0f;
        }

        swapRoom += Time.deltaTime;
    }
}
