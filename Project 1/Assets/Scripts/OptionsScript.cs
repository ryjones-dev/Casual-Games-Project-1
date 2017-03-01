using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour {
    
    public GameObject settingsPanel; //panel to show when menu is open
    public GameObject escPanel;      //panel to show when menu is closed

    public bool gamePaused;

    //sliders and toggleboxes, and variables to store their last-held data
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

    private CursorLockMode prevLockMode; // store previous lockmode, unlock cursor when menu is open and resume lock when closed

    private Canvas canvas;

    // Use this for initialization
    void Start () {
        
        settingsPanel.SetActive(false);
        escPanel.SetActive(true);

        //audio is not yet implemented //disable audio settings
        //leaving audio settings enabled for now, to show off options menu
        //music.enabled = false;
        //sound.enabled = false;

        mouse.value = mouseSensitivity;
        invertMouseMovement.isOn = mouseMovementInverted;
        invertMouseRotation.isOn = mouseRotationInverted;
        music.value = musicVolume;
        sound.value = soundEffectVolume;

        canvas = gameObject.GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        if (canvas.enabled) //do nothing if options menu is not "active"
        {
            if (Input.GetKeyDown("escape")) //esc opens and closes options menu
            {
                if(gamePaused) //close menu if it is open
                //if (settingsPanel.activeSelf) 
                {
                    CancelSettings();
                }
                else //open menu if it is closed
                {
                    Cursor.visible = true;
                    prevLockMode = Cursor.lockState;
                    Cursor.lockState = CursorLockMode.None;

                    settingsPanel.SetActive(true);
                    escPanel.SetActive(false);
                }
                gamePaused = !gamePaused;
            }
        }
        else if(gamePaused)
        //else if (settingsPanel.activeSelf) //if options menu was deactivated, cancel settings
        {
            CancelSettings();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gamePaused = false;
        }
    }

    //apply new settings and close menu
    public void ChangeSettings()
    {
        Cursor.visible = false;
        Cursor.lockState = prevLockMode;

        mouseSensitivity = mouse.value;
        mouseMovementInverted = invertMouseMovement.isOn;
        mouseRotationInverted = invertMouseRotation.isOn;
        musicVolume = music.value;
        soundEffectVolume = sound.value;
        settingsPanel.SetActive(false);
        escPanel.SetActive(true);

        //deselect button, otherwise last-hit button will appear to be highlighted until a new button is hit, 
        //seems like weird default behavior personally
        EventSystem.current.SetSelectedGameObject(null);
    }

    //close settings menu, revert settings to last accepted values
    public void CancelSettings()
    {
        Cursor.visible = false;
        Cursor.lockState = prevLockMode;

        mouse.value = mouseSensitivity;
        invertMouseMovement.isOn = mouseMovementInverted;
        invertMouseRotation.isOn = mouseRotationInverted;
        music.value = musicVolume;
        sound.value = soundEffectVolume;
        settingsPanel.SetActive(false);
        escPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
