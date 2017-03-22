using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    public static void OnWin(DEL_GAME_WIN del)
    {

    }

    public delegate void DEL_GAME_WIN();
    public List<DEL_GAME_WIN> m_onGameWon = new List<DEL_GAME_WIN>();


    
    public void handleGameWon()
    {

    }
    [SerializeField]
    OptionsScript m_optionScript;
    [SerializeField]
    PickUp m_pickUp;
    [SerializeField]
    Hand hand;
    private void Update()
    {
        if(GameSettings.STATE == GameSettings.GAME_STATE.PAUSED  || GameSettings.STATE == GameSettings.GAME_STATE.FROZEN)
            return;
        m_pickUp.kUpdate();
        hand.kUpdate(GameSettings.MOUSE_SENSITIVITY_HORIZONTAL, GameSettings.MOUSE_SENSITIVITY_VERTICAL, GameSettings.MOUSE_SENSITIVITY_HORIZONTAL, GameSettings.IS_MOUSE_INPUT_INVERTED);

    }
}
