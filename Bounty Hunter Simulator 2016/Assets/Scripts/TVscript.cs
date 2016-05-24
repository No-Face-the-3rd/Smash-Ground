using UnityEngine;
using System.Collections;

public class TVscript : MonoBehaviour
{
   
    private int colorChange;
    private Material material;

	void Start () {

        colorChange = 0;

        foreach(Material matt in GetComponent<Renderer>().materials)
        {
            if (matt.name == "prop_ad (Instance)")
            {
                material = matt;
                break;
            }
        }
	}

	void Update () {
        colorChange++;
        if (colorChange > 10)
        {
            material.color = Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f);
            colorChange = 0;
        }

	}
}
