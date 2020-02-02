using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource bearSource;
    public AudioSource sfxSource;
    public AudioSource orderSource;

    public AudioClip[] bearSounds;
    public AudioClip[] sfxSounds;
    public AudioClip[] orderSounds;
    public Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //audio files must be added to sfxSounds in Unity editor in the same order as following

        sfxDict.Add("conveyorBelt", sfxSounds[0]);
        sfxDict.Add("orderRight", sfxSounds[1]);
        sfxDict.Add("orderWrong", sfxSounds[2]);
        sfxDict.Add("icecreamDrop", sfxSounds[3]);
        sfxDict.Add("icecreamSquirt", sfxSounds[4]);
    }
    void Update()
    {
        //test

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.instance.PlayBearSound();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.instance.PlaySFX("orderRight");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SoundManager.instance.PlaySFX("orderWrong");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SoundManager.instance.PlaySFX("conveyorBelt");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SoundManager.instance.PlaySFX("icecreamSquirt");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SoundManager.instance.PlaySFX("icecreamDrop");
        }


    }
    public void PlayBearSound()
    {
        bearSource.Stop();
        bearSource.clip = bearSounds[Random.Range(0, bearSounds.Length)];
        bearSource.Play();
    }
    public void PlaySFX(string name) //parameters are dict keys
    {
        sfxSource.Stop();
        if (sfxDict.ContainsKey(name))
        {
            sfxSource.clip = sfxDict[name];
            sfxSource.Play();
        } else
        {
            Debug.Log("clip "+ name + " does not exist");
        }
    }
    public void PlayOrderSound(bool success) 
    {
        if (success)
        {
            orderSource.clip = sfxDict["orderRight"];
            orderSource.Play();
        } else
        {
            orderSource.clip = sfxDict["orderWrong"];
            orderSource.Play();
        }
    }
}