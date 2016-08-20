using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioMixManager : MonoBehaviour {
    public AudioMixer audioMixer;

    public static AudioMixManager audioMixMan;

    private GameObject soundsCanvas;
    void Awake()
    {
        if(audioMixMan == null)
        {
            DontDestroyOnLoad(gameObject);
            audioMixMan = this;
        }
        else if ( audioMixMan != this)
        {
            Destroy(gameObject);
        }
    }


    public void setMaster(float masterLevel)
    {
        audioMixer.SetFloat("Master Volume", masterLevel);
    }
    public float getMasterVolume()
    {
        float tmp;
        audioMixer.GetFloat("Master Volume", out tmp);
        return tmp;
    }

    public void setPlayers(float playerLevel)
    {
        audioMixer.SetFloat("Player Volume", playerLevel);
    }
    public float getPlayerVolume()
    {
        float tmp;
        audioMixer.GetFloat("Player Volume", out tmp);
        return tmp;
    }

    public void setEnemies(float enemyLevel)
    {
        audioMixer.SetFloat("Enemy Volume", enemyLevel);
    }
    public float getEnemyVolume()
    {
        float tmp;
        audioMixer.GetFloat("Enemy Volume", out tmp);
        return tmp;
    }

    public void setEnvironment(float enviroLevel)
    {
        audioMixer.SetFloat("Environment Volume", enviroLevel);
    }
    public float getEnvironmentVolume()
    {
        float tmp;
        audioMixer.GetFloat("Environment Volume", out tmp);
        return tmp;
    }

    public void setBullets(float bulletLevel)
    {
        audioMixer.SetFloat("Bullet Volume", bulletLevel);
    }
    public float getBulletVolume()
    {
        float tmp;
        audioMixer.GetFloat("Bullet Volume", out tmp);
        return tmp;
    }

    public void setMusic(float musicLevel)
    {
        audioMixer.SetFloat("Music Volume", musicLevel);
    }
    public float getMusicVolume()
    {
        float tmp;
        audioMixer.GetFloat("Music Volume", out tmp);
        return tmp;
    }

    public void setToDefault()
    {
        audioMixer.ClearFloat("Master Volume");
        audioMixer.ClearFloat("Player Volume");
        audioMixer.ClearFloat("Enemy Volume");
        audioMixer.ClearFloat("Environment Volume");
        audioMixer.ClearFloat("Bullet Volume");
        audioMixer.ClearFloat("Music Volume");
    }


    public void setSoundsCanvas(GameObject canvas)
    {
        soundsCanvas = canvas;
    }
    public GameObject getSoundsCanvas()
    {
        return soundsCanvas;
    }

}
