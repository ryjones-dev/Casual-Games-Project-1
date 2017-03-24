using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour {
    public GameObject particleSys;
 //   public GameObject gameManager;
    public Game gameScript;
    private float timePassed;
    private bool hasPlayed = false;

    //585201444
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (hasPlayed)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= 5.0f)
            {
                destroyPart();
            }
        }

        if (!hasPlayed)
        {
            gameScript.addGameWonDelayedHandler(playWinParticle);
        }
    }

    private void playWinParticle()
    {
        if (!particleSys.GetComponent<ParticleSystem>().isPlaying)
        {
            particleSys.GetComponent<ParticleSystem>().Play();
        }
    }

    private void destroyPart()
    {
        Destroy(particleSys);
    }
}
