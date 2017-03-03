using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private Canvas optionsMenu;

    private bool gamePaused = false;
    public static bool Paused { get { return instance.gamePaused; } }

    private static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        GameObject optionsObject = this.transform.Find("OptionsMenu").gameObject;
        optionsMenu = optionsObject.GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void Update () {
		if(SceneManager.GetActiveScene().name == "Title"){
            optionsMenu.enabled = false;
        }
        else
        {
            optionsMenu.enabled = true;
        }
	}

    public static void Pause()
    {
        instance.gamePaused = true;
        Debug.Log("Game paused");
    }

    public static void Unpause()
    {
        instance.gamePaused = false;
        Debug.Log("Game unpaused");
    }
}
