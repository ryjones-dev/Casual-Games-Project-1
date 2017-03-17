using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUIScript : MonoBehaviour {

    private Canvas canvas;

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
    private float timeRemaining;
    

	// Use this for initialization
	void Start () {
        timeRemaining = timeInitial;

        //set up score UI to initial values
        progressFullScale = progressBar.transform.localScale.x;
        progressCenterX = progressBar.transform.localPosition.x;
        quotaText.text = "/"+scoreQuota;
        UpdateScoreUI();

    }
	
	// Update is called once per frame
	void Update () {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0) { timeRemaining = 0; }
            UpdateTimerUI();
        }

        //if score has changed, modify score UI
        if (score != scoreLast)
        {
            UpdateScoreUI();
        }
    }

    //updates timer UI to show time remaining in the user-friendly minute:second format
    private void UpdateTimerUI()
    {
        timerMinutes = (int)timeRemaining / 60;
        timerSeconds = (int)timeRemaining % 60;
        string secondstring = "" + timerSeconds;
        if (timerSeconds < 10) { secondstring = "0" + secondstring; }
        timer.text = timerMinutes + ":" + secondstring;
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
}
