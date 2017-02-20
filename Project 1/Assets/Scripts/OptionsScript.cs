using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsScript : MonoBehaviour {

    //Canvas optionsMenu;

    public GameObject settingsPanel;
    public GameObject escPanel;
    public Slider mouse;
    public float mouseSensitivity=1.0f;
    public Slider music;
    public float musicVolume = 1.0f;
    public Slider sound;
    public float soundEffectVolume = 1.0f;
    public Toggle invertMouseMovement;
    public bool mouseMovementInverted = false;
    public Toggle invertMouseRotation;
    public bool mouseRotationInverted = false;

    private CursorLockMode prevLockMode;

    // Use this for initialization
    void Start () {

        //optionsMenu = GetComponent<Canvas>();
        //optionsMenu.enabled = false;
        settingsPanel.SetActive(false);
        escPanel.SetActive(true);

        //audio is not yet implemented
        //music.enabled = false;
        //sound.enabled = false;

        mouse.value = mouseSensitivity;
        invertMouseMovement.isOn = mouseMovementInverted;
        invertMouseRotation.isOn = mouseRotationInverted;
        music.value = musicVolume;
        sound.value = soundEffectVolume;

        //optionsMenu = GameObject.Find("OptionsMenu").GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            //escPanel.SetActive(!escPanel.activeSelf);
            //if (optionsMenu.enabled)
            if(settingsPanel.activeSelf)
            {
                CancelSettings();
            }
            else
            {
                Cursor.visible = true;
                prevLockMode = Cursor.lockState;
                Cursor.lockState = CursorLockMode.None;

                settingsPanel.SetActive(true);
                escPanel.SetActive(false);
                //optionsMenu.enabled = true;
            }

           // optionsMenu.enabled = true;
            //optionsMenu.enabled = !optionsMenu.enabled;
        }
    }

    public void ChangeSettings()
    {
        Cursor.visible = false;
        Cursor.lockState = prevLockMode;

        mouseSensitivity = mouse.value;
        mouseMovementInverted = invertMouseMovement.isOn;
        mouseRotationInverted = invertMouseRotation.isOn;
        musicVolume = music.value;
        soundEffectVolume = sound.value;
        //optionsMenu.enabled = false;
        settingsPanel.SetActive(false);
        escPanel.SetActive(true);

        //deselect button, otherwise last-hit button will appear to be highlighted until a new button is hit, 
        //seems like weird default behavior personally
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void CancelSettings()
    {
        Cursor.visible = false;
        Cursor.lockState = prevLockMode;

        mouse.value = mouseSensitivity;
        invertMouseMovement.isOn = mouseMovementInverted;
        invertMouseRotation.isOn = mouseRotationInverted;
        music.value = musicVolume;
        sound.value = soundEffectVolume;
        //optionsMenu.enabled = false;
        settingsPanel.SetActive(false);
        escPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
    }
}
