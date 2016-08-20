using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundsUIScript : MonoBehaviour {

    private Slider[] sliders;

	// Use this for initialization
	void Start () {
        AudioMixManager.audioMixMan.setSoundsCanvas(gameObject);
        sliders = GetComponentsInChildren<Slider>();
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void resetDefaults()
    {
        AudioMixManager.audioMixMan.setToDefault();
        StartCoroutine(slidersRT());
    }

    IEnumerator slidersRT()
    {
        yield return 0;
        setSliders();
    }

    public void setSliders()
    {
        getSlider(0).value = AudioMixManager.audioMixMan.getMasterVolume();
        getSlider(1).value = AudioMixManager.audioMixMan.getPlayerVolume();
        getSlider(2).value = AudioMixManager.audioMixMan.getEnemyVolume();
        getSlider(3).value = AudioMixManager.audioMixMan.getEnvironmentVolume();
        getSlider(4).value = AudioMixManager.audioMixMan.getBulletVolume();
        getSlider(5).value = AudioMixManager.audioMixMan.getMusicVolume();
     }

    public Slider getSlider(int index)
    {
        return sliders[index];
    }
    public int getNumSliders()
    {
        return sliders.Length;
    }
    public void enterPauseMenu()
    {
        PauseMenuManager.pauseManager.getPauseCanvas().SetActive(true);
        PauseMenuManager.pauseManager.getPauseCanvas().GetComponent<PauseUIScript>().getButton(0).Select();
        gameObject.SetActive(false);
    }

    public void setMaster(float masterLevel)
    {
        AudioMixManager.audioMixMan.setMaster(masterLevel);
    }
    
    public void setPlayers(float playerLevel)
    {
        AudioMixManager.audioMixMan.setPlayers(playerLevel);
    }

    public void setEnemies(float enemyLevel)
    {
        AudioMixManager.audioMixMan.setEnemies(enemyLevel);
    }

    public void setEnvironment(float enviroLevel)
    {
        AudioMixManager.audioMixMan.setEnvironment(enviroLevel);
    }

    public void setBullets(float bulletLevel)
    {
        AudioMixManager.audioMixMan.setBullets(bulletLevel);
    }

    public void setMusic(float musicLevel)
    {
        AudioMixManager.audioMixMan.setMusic(musicLevel);
    }
}
