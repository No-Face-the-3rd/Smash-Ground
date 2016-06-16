using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VignetteTVEffect: MonoBehaviour
{

    public int minH, maxH;
    private RawImage vignette;

	void Start ()
    {
        vignette = GetComponent<RawImage>();
	}
	

	void Update ()
    {
        vignette.uvRect = new Rect(vignette.uvRect.x, vignette.uvRect.y, vignette.uvRect.width, Random.Range(minH, maxH));
      
	}
}
