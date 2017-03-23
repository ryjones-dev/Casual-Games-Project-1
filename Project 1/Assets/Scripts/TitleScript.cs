using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

    public GameObject persistent;
    public string levelName;

    private OptionsScript options;
    private GameUIScript game;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
		
        if(GameObject.Find("Persistent") == null)
        {
            GameObject per = GameObject.Instantiate(persistent);
            per.name = "Persistent";
            DontDestroyOnLoad(per);
            per.transform.SetAsLastSibling();
        }
        
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(options == null)
        {
            options = OptionsScript.instance;
        }
        else
        {
            audio.volume = options.musicVolume;
        }
        if (game == null)
        {
            game = GameUIScript.instance;
        }


    }

    public void LoadFirstLevel()
    {
        game.SetGoalAndTime(5000, 60);
        SceneManager.LoadScene(levelName);
    }
}
