using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GraphicsUIScript : MonoBehaviour {

    private Dropdown resolutionsDrop;


	// Use this for initialization
	void Start () {
        GraphicsMenuManager.graphicsManager.setGraphicsCanvas(gameObject);
        resolutionsDrop = GetComponentInChildren<Dropdown>();
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void enterPauseMenu()
    {
        PauseMenuManager.pauseManager.getPauseCanvas().SetActive(true);
        PauseMenuManager.pauseManager.getPauseCanvas().GetComponent<PauseUIScript>().getButton(PauseMenuManager.pauseManager.getPauseCanvas().GetComponent<PauseUIScript>().getNumButtons() - 1).Select();
        PauseMenuManager.pauseManager.getPauseCanvas().GetComponent<PauseUIScript>().getButton(0).Select();
        gameObject.SetActive(false);
    }

    public void setSettings()
    {

    }
}
