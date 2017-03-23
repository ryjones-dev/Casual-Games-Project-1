﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour {

    public static OptionsScript instance;

    public GameObject settingsPanel; //panel to show when menu is open
    public GameObject escPanel;      //panel to show when menu is closed

    //sliders and toggleboxes, and variables to store their last-held data
    public Slider mouse;
    public Slider music;
    public float musicVolume = 1.0f;
    public Slider sound;
    public float soundEffectVolume = 1.0f;
    public Toggle invertMouseMovement;
    public bool mouseMovementInverted = false;
    public Toggle invertMouseRotation;
    public bool mouseRotationInverted = false;

    private CursorLockMode prevLockMode; // store previous lockmode, unlock cursor when menu is open and resume lock when closed
    private bool prevCursorVisible;

    private Canvas canvas;

    public enum OPTION_STATE {CLOSED,OPEN};
    GameSettings.GAME_STATE m_previousGameState;
    OPTION_STATE m_state = OPTION_STATE.CLOSED;


    // Use this for initialization
    void Start () {
        if(instance == null)
        {
            instance = this;
        }

        settingsPanel.SetActive(false);
        escPanel.SetActive(true);

        //audio is not yet implemented //disable audio settings
        //leaving audio settings enabled for now, to show off options menu
        //music.enabled = false;
        //sound.enabled = false;

        mouse.value = GameSettings.MOUSE_SENSITIVITY;
        invertMouseMovement.isOn = mouseMovementInverted;
        invertMouseRotation.isOn = mouseRotationInverted;
        music.value = musicVolume;
        sound.value = soundEffectVolume;

        canvas = gameObject.GetComponent<Canvas>();
	}
    void setState(OPTION_STATE state)
    {
        if(m_state == state) return;
        m_state = state;
        if (m_state == OPTION_STATE.OPEN)
            openOption();
        else closeOption();
    }

    void openOption()
    {
        //canvas.enabled = true;
        prevCursorVisible = Cursor.visible;
        Cursor.visible = true;
        prevLockMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;

        settingsPanel.SetActive(true);
        escPanel.SetActive(false);

        m_previousGameState = GameSettings.STATE;
        GameSettings.STATE = GameSettings.GAME_STATE.FROZEN;

        setState(OPTION_STATE.OPEN);
    }
    void closeOption()
    {
        //canvas.enabled = false;
        Cursor.visible = prevCursorVisible;
        Cursor.lockState = prevLockMode;

        settingsPanel.SetActive(false);
        escPanel.SetActive(true);

        GameSettings.STATE = m_previousGameState;
        setState(OPTION_STATE.CLOSED);
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("escape")) //esc opens and closes options menu
        {
            if (m_state == OPTION_STATE.CLOSED)
                setState(OPTION_STATE.OPEN);
            else
            {
                setState(OPTION_STATE.CLOSED);

                CancelSettings();
            }

        }
        
    }

    //apply new settings and close menu
    public void ChangeSettings()
    {
        //Cursor.visible = false;
        //Cursor.lockState = prevLockMode;

        GameSettings.MOUSE_SENSITIVITY = mouse.value;
        mouseMovementInverted = invertMouseMovement.isOn;
        mouseRotationInverted = invertMouseRotation.isOn;
        musicVolume = music.value;
        soundEffectVolume = sound.value;
        //settingsPanel.SetActive(false);
        //escPanel.SetActive(true);

        //deselect button, otherwise last-hit button will appear to be highlighted until a new button is hit, 
        //seems like weird default behavior personally
        EventSystem.current.SetSelectedGameObject(null);
        closeOption();
    }

    //close settings menu, revert settings to last accepted values
    public void CancelSettings()
    {
        mouse.value = GameSettings.MOUSE_SENSITIVITY;
        invertMouseMovement.isOn = mouseMovementInverted;
        invertMouseRotation.isOn = mouseRotationInverted;
        music.value = musicVolume;
        sound.value = soundEffectVolume;

        EventSystem.current.SetSelectedGameObject(null);
        closeOption();
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
        CancelSettings();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
