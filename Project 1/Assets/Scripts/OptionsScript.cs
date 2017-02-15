using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour {

    Canvas optionsMenu;

    public Slider mouse;
    public float mouseSensitivity=1.0f;
    public Slider music;
    public float musicVolume = 1.0f;
    public Slider sound;
    public float soundEffectVolume = 1.0f;
    public Toggle invertMouse;
    public bool mouseInverted = false;

	// Use this for initialization
	void Start () {
        
        optionsMenu = GetComponent<Canvas>();
        optionsMenu.enabled = false;

        //audio is not yet implemented
        music.enabled = false;
        sound.enabled = false;

        mouse.value = mouseSensitivity;
        invertMouse.isOn = mouseInverted;
        music.value = musicVolume;
        sound.value = soundEffectVolume;

        //optionsMenu = GameObject.Find("OptionsMenu").GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            if (optionsMenu.enabled)
            {
                CancelSettings();
            }
            else
            {
                optionsMenu.enabled = true;
            }
            //optionsMenu.enabled = !optionsMenu.enabled;
        }
    }

    public void ChangeSettings()
    {
        mouseSensitivity = mouse.value;
        mouseInverted = invertMouse.isOn;
        musicVolume = music.value;
        soundEffectVolume = sound.value;
        optionsMenu.enabled = false;
    }

    public void CancelSettings()
    {
        mouse.value = mouseSensitivity;
        invertMouse.isOn = mouseInverted;
        music.value = musicVolume;
        sound.value = soundEffectVolume;
        optionsMenu.enabled = false;
    }
}
