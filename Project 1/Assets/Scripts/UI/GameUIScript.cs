using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUIScript : MonoBehaviour {

    public static GameUIScript instance;

    private Canvas canvas;
    private OptionsScript options;
    private Game game;
    private GameObject gameObject;
    private bool gameSet = false;
    private bool newLevel=false;

    public GameObject victoryPanel;
    public Text finalScoreText;

    public Text timer;
    public float timeInitial = 70;
    public Text goal;

    public Text scoreText;
    public int score = 0;
    private int scoreLast;
    public Text quotaText;
    public int scoreQuota = 5000;

    public GameObject progressBar;
    public float progressFullScale;
    public float progressCenterX;

    private int timerMinutes;
    private int timerSeconds;
    private int secondsPrevious=0;
    private float timeRemaining;

    public int lastSceneIndex;

    private bool levelIsOver;

    private AudioSource audio;
    public AudioClip timerTick;

	// Use this for initialization
	void Start () {
        if(instance == null)
        {
            instance = this;
        }

        canvas = GetComponent<Canvas>();
        audio = GetComponent<AudioSource>();
        options = OptionsScript.instance;
        //options = GameObject.Find("OptionsMenu").GetComponent<OptionsScript>();
        timeRemaining = timeInitial;
        levelIsOver = false;

        //set up score UI to initial values
        progressFullScale = progressBar.transform.localScale.x;
        progressCenterX = progressBar.transform.localPosition.x;
        quotaText.text = "/"+scoreQuota;
        UpdateScoreUI();
        UpdateTimerUI();

        //AddEventHandler(LevelEnd);
    }

    // Update is called once per frame
    void Update() {
        if (newLevel)
        {
            newLevel = false;
            gameSet = false;
        }
        if(gameObject == null)
        {
            gameObject = GameObject.Find("Game");
            
        }
        else if (game == null)
        {
            game = gameObject.GetComponent<Game>();
        }
        else if(gameSet == false)
        {
            game.addGameWonHandler(LevelEnd);
            gameSet = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0) //no ui on title screen
        {
            canvas.enabled = false;
            return;
        }
        else
        {
            canvas.enabled = true;
        }
        if(GameSettings.STATE == GameSettings.GAME_STATE.PLAYING && !levelIsOver){
            if (timeRemaining > 0)
            {
                lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
                timeRemaining -= Time.deltaTime;
                if (timeRemaining < 0) { timeRemaining = 0; }
                if((int)timeRemaining != secondsPrevious)
                {
                    audio.PlayOneShot(timerTick, options.soundEffectVolume / 5);
                    secondsPrevious = (int)timeRemaining;
                }
                UpdateTimerUI();
            }

            //if score has changed, modify score UI
            if (score != scoreLast)
            {
                UpdateScoreUI();
            }
        }
        /* //instant win debug command
        if (Input.GetKeyDown("p"))
        {
            LevelEnd();
        }*/
    }

    //updates timer UI to show time remaining in the user-friendly minute:second format
    private void UpdateTimerUI()
    {
        timerMinutes = (int)timeRemaining / 60;
        timerSeconds = (int)timeRemaining % 60;
        string secondstring = "" + timerSeconds;
        if (timerSeconds < 10) { secondstring = "0" + secondstring; }
        timer.text = timerMinutes + ":" + secondstring;

        if(timeRemaining <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void UpdateScoreUI() {
        Vector3 newScale = progressBar.transform.localScale;
        Vector3 newPos = progressBar.transform.localPosition;

        if (score < scoreQuota)
        {
            newScale.x = progressFullScale * score / scoreQuota;
            newPos.x = progressCenterX -progressFullScale*211/2 + 211/2*newScale.x;

            //change colors of text based on relation to quota
            scoreText.color = Color.yellow;
        }
        else
        {
            newScale.x = progressFullScale;
            newPos.x = progressCenterX;

            scoreText.color = Color.green;
        }
        progressBar.transform.localScale = newScale;
        progressBar.transform.localPosition = newPos;
        
       

        scoreText.text = "" + score;
        while(scoreText.text.Length < quotaText.text.Length - 1)
        {
            scoreText.text = "0" + scoreText.text;
        }
        
        //score is once again up to date
        scoreLast = score;
    }

    public void SetGoalAndTime(int scoreGoal, float seconds)
    {
        timeRemaining = seconds;
        score = 0;
        scoreQuota = scoreGoal;
        UpdateScoreUI();
        UpdateTimerUI();
    }

    public void LevelEnd()
    {
        if (levelIsOver == false)
        {
            levelIsOver = true;
            victoryPanel.SetActive(true);
            score += timerSeconds * 10;
            finalScoreText.text = "" + score;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void LoadNextLevel()
    {
        
        SceneManager.LoadScene(lastSceneIndex + 1);
        SetGoalAndTime(5000, 60);
        // levelIsOver = false;
        newLevel = true;
        victoryPanel.SetActive(false);
        gameSet = false;
    }
}
