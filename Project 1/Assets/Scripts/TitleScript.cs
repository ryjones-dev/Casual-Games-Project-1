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
    public AudioClip titleBGM;

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
        options = GameObject.Find("OptionsMenu").GetComponent<OptionsScript>();
        game = GameObject.Find("GameUI").GetComponent<GameUIScript>();
        audio.volume = options.musicVolume;
        /*
        audio.clip = titleBGM;
        audio.playOnAwake=true;
        */
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadFirstLevel()
    {
        game.SetGoalAndTime(5000, 1);
        SceneManager.LoadScene(levelName);
    }
}
