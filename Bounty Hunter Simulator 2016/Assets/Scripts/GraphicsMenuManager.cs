using UnityEngine;
using System.Collections;

public class GraphicsMenuManager : MonoBehaviour {

    public static GraphicsMenuManager graphicsManager;

    private GameObject graphicsCanvas;

    void Awake()
    {
        if(graphicsManager == null)
        {
            DontDestroyOnLoad(gameObject);
            graphicsManager = this;
        }
        else if(graphicsManager != this)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void setGraphicsCanvas(GameObject canvas)
    {
        graphicsCanvas = canvas;
    }
    public GameObject getGraphicsCanvas()
    {
        return graphicsCanvas;
    }


}
