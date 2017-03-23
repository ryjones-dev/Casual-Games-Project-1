using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    private AudioSource audio;
    private GameObject optionMenu;
    private OptionsScript options;
    
    public AudioClip music;
    public float volumeScale=1.0f;

    private float lastVolume = 0.0f;

	// Use this for initialization
	void Start () {
        audio=GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(optionMenu == null)
        {
            optionMenu = GameObject.Find("OptionsMenu");
        }
        else if(options == null)
        {
            options = optionMenu.GetComponent<OptionsScript>();
            audio.PlayOneShot(music, volumeScale * options.musicVolume);
        }
        else if(lastVolume != options.musicVolume)
        {
            audio.volume = volumeScale * options.musicVolume;
            lastVolume = options.musicVolume;
        }
	}
}
