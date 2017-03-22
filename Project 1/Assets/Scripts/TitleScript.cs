using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

    public GameObject persistent;
    public string levelName;

    private OptionsScript options;
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
        SceneManager.LoadScene(levelName);
    }
}
