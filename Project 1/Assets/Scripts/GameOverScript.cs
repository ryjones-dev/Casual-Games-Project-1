using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    private GameUIScript game;

    // Use this for initialization
    void Start () {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


        game = GameObject.Find("GameUI").GetComponent<GameUIScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadFirstLevel()
    {
        game.SetGoalAndTime(5000 * (game.lastSceneIndex + 1), 60);
        SceneManager.LoadScene(GameUIScript.instance.lastSceneIndex);
    }

    public void LoadTitleScreen()
    {

        SceneManager.LoadScene("Title");
    }
}
