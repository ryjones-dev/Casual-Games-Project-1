using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    public delegate void ON_GAME_WON();
    public delegate void ON_GAME_WON_DELAYED();
    List<ON_GAME_WON> m_onGameWon = new List<ON_GAME_WON>();
    List<ON_GAME_WON_DELAYED> m_onGameWonDelayed = new List<ON_GAME_WON_DELAYED>();

    const float m_checkDelayedInit = 3.0f;
    float m_checkDelayTime = 3.0f;

    public enum GAME_WON_CHECKING_STATE { PANDING, REGISTERED, FINISHED };
    GAME_WON_CHECKING_STATE m_gameCheckState = GAME_WON_CHECKING_STATE.PANDING;
    

    public void addGameWonHandler(ON_GAME_WON onGameWon)
    {
        m_onGameWon.Add(onGameWon);
    }
    public void addGameWonDelayedHandler(ON_GAME_WON_DELAYED onGameWon)
    {
        m_onGameWonDelayed.Add(onGameWon);
    }
    void raiseGameWon()
    {
        Debug.Log("raiseGameWon");
        for (int i = m_onGameWon.Count - 1; i >= 0; i--)
        {
            m_onGameWon[i]();
        }
    }
    void raiseGameWonDelayed()
    {
        Debug.Log("raiseGameWonDelayed");
        for (int i = m_onGameWonDelayed.Count - 1; i >= 0; i--)
        {
            m_onGameWonDelayed[i]();
        }
    }

    [SerializeField]
    Transform m_suitCaseUpperPart;
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
          hand.kUpdate(GameSettings.MOUSE_SENSITIVITY, GameSettings.IS_MOUSE_INPUT_INVERTED, m_pickUp.isHandEnabled());
        //Debug.Log(m_gameCheckState);
        switch (m_gameCheckState)
        {
            case GAME_WON_CHECKING_STATE.PANDING:
                checkGameWon();
                break;
            case GAME_WON_CHECKING_STATE.REGISTERED:
                m_checkDelayTime -= Time.deltaTime;
                if(m_checkDelayTime <= 0)
                {
                    raiseGameWonDelayed();
                    m_gameCheckState = GAME_WON_CHECKING_STATE.FINISHED;
                }
                break;
            case GAME_WON_CHECKING_STATE.FINISHED:
                raiseGameWon();
                break;
        }
        



    }
    void checkGameWon()
    {
        bool isScoreGoodEnough = GameUIScript.instance.score >= GameUIScript.instance.scoreQuota;
        bool isSuitCaseClosed = m_suitCaseUpperPart.localRotation.eulerAngles.x < 50.0 && m_suitCaseUpperPart.localRotation.eulerAngles.x > -50.0f;
        Debug.Log(isScoreGoodEnough + ", " + isSuitCaseClosed);
        if (isScoreGoodEnough && isSuitCaseClosed)
        {
            m_gameCheckState = GAME_WON_CHECKING_STATE.REGISTERED;
            m_checkDelayTime = m_checkDelayedInit;
        }
    }
          
}
