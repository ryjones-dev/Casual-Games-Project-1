using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

    public GameObject persistent;

	// Use this for initialization
	void Start () {
		
        if(GameObject.Find("Persistent") == null)
        {
            GameObject per = GameObject.Instantiate(persistent);
            per.name = "Persistent";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("level_1");
    }
}
