using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour {
    public GameObject particleSys;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void playWinParticle()
    {
        if (!particleSys.GetComponent<ParticleSystem>().isPlaying)
        {
            particleSys.GetComponent<ParticleSystem>().Play();
        }
    }
}
