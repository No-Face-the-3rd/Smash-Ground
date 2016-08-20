using UnityEngine;
using System.Collections;

public class PauseMenuManager : MonoBehaviour {

    public static PauseMenuManager pauseManager;

    private GameObject pauseCanvas;
    private bool isPaused;
    private int highlighted;

    void Awake()
    {
        if (pauseManager == null)
        {
            DontDestroyOnLoad(gameObject);
            pauseManager = this;
        }
        else if (pauseManager != this)
        {
            Destroy(gameObject);
        }
        highlighted = 0;
    }

	// Use this for initialization
	void Start () {
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Escape"))
        {
            isPaused = !isPaused;
            doPauseStuff();
        }
	}

    public void setPauseCanvas(GameObject pause)
    {
        pauseCanvas = pause;
    }
    public GameObject getPauseCanvas()
    {
        return pauseCanvas;
    }

    public void setPaused(bool pause)
    {
        isPaused = pause;
        doPauseStuff();
    }

    private void doPauseStuff()
    {
        if (isPaused)
        {
            Time.timeScale = 0.0f;
            if (pauseCanvas != null)
            {
                pauseCanvas.SetActive(true);
                pauseCanvas.GetComponent<PauseUIScript>().getButton(pauseCanvas.GetComponent<PauseUIScript>().getNumButtons() - 1).Select();
                highlighted = 0;
                pauseCanvas.GetComponent<PauseUIScript>().getButton(highlighted).Select();
            }
        }
        else
        {
            Time.timeScale = 1.0f;
            if (pauseCanvas != null)
            {
                pauseCanvas.SetActive(false);
                AudioMixManager.audioMixMan.getSoundsCanvas().SetActive(false);
            }
        } 

    }
}
