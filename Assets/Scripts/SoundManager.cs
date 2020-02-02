using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource sfxSource;
    public AudioSource bearSource;

    public AudioClip[] bearSounds;
    public AudioClip[] sfxSounds;
    public Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    void Start()
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

        //audio files must be added to sfxSounds in Unity editor in the same order as following

        sfxDict.Add("conveyorBelt", sfxSounds[0]);
        sfxDict.Add("orderRight", sfxSounds[1]);
        sfxDict.Add("orderWrong", sfxSounds[2]);
        sfxDict.Add("icecreamDrop", sfxSounds[3]);
        sfxDict.Add("icecreamSquirt", sfxSounds[4]);
    }
    void Update()
    {

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
}