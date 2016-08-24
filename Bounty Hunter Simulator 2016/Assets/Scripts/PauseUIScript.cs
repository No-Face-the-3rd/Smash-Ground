using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseUIScript : MonoBehaviour {

    private Button[] buttons;

    void Awake()
    {
    }

	// Use this for initialization
	void Start () {
        PauseMenuManager.pauseManager.setPauseCanvas(gameObject);
        buttons = GetComponentsInChildren<Button>();
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void doResume()
    {
        PauseMenuManager.pauseManager.setPaused(false);
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void enterSoundMenu()
    {
        AudioMixManager.audioMixMan.getSoundsCanvas().SetActive(true);
        AudioMixManager.audioMixMan.getSoundsCanvas().GetComponent<SoundsUIScript>().setSliders();
        AudioMixManager.audioMixMan.getSoundsCanvas().GetComponent<SoundsUIScript>().getSlider(AudioMixManager.audioMixMan.getSoundsCanvas().GetComponent<SoundsUIScript>().getNumSliders() - 1);
        AudioMixManager.audioMixMan.getSoundsCanvas().GetComponent<SoundsUIScript>().getSlider(0).Select();
        gameObject.SetActive(false);
     }

    public void enterGraphicsMenu()
    {
        GraphicsMenuManager.graphicsManager.getGraphicsCanvas().SetActive(true);
        gameObject.SetActive(false);
    }

    public int getNumButtons()
    {
        return buttons.Length;
    }
    public Button getButton(int index)
    {
        return buttons[index];
    }
}
