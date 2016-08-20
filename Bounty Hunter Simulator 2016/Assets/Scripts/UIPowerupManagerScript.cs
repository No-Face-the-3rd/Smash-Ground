using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPowerupManagerScript : MonoBehaviour
{
    public RawImage[] powerupDisplay;
    public Texture[] textures;
    private bool front;
    private float flipTime;


    void Start ()
    {
        front = true;
        flipTime = 0.0f;
	}


    void Update()
    {
        for (int i = 0; i < PlayerLocator.locator.players.Length; i++)
        {
            int idTmp = PlayerLocator.locator.players[i].GetComponent<PlayerController>().playerNum - 1;
            if (idTmp < powerupDisplay.Length)
            {
                int textureID = ((int)PlayerLocator.locator.players[i].GetComponent<PlayerController>().powerup * 2 - (front ? 1 : 0));
                textureID = textureID < 0 ? 0 : textureID;
                powerupDisplay[idTmp].texture = textures[textureID];
            }
        }

        if(flipTime >= 0.5f)
        {
            front = !front;
            flipTime = 0.0f;
        }

        flipTime += Time.deltaTime;
    }
}
