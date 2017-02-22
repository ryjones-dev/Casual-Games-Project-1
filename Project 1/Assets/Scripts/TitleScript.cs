using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

    public GameObject persistant;

	// Use this for initialization
	void Start () {
		
        if(GameObject.Find("Persistant") == null)
        {
            GameObject per = GameObject.Instantiate(persistant);
            per.name = "Persistant";
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
