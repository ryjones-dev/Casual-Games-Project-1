using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionTest : MonoBehaviour {
    static float CHECK_INTERVAL = 2.0f; //used to check the winning condition each X amount of second
    public ColliderDetector m_detector; 
    public List<GameObject> m_objs; // game objects that need to be stored in a box
                                    // Use this for initialization
    public GameObject m_winningScreen;
    float m_timeElapsed = 0;
	void Start () {
		
	}
	void hdrCheckWinningCondition(List<Collider> collider)
    {
        for(int i = 0; i < m_objs.Count; i++)
        {
            var obj = m_objs[i];
            bool isPresent = false;
            for(int j = 0; j < collider.Count; j++)
            {
               if(collider[j].gameObject == obj)
                {
                    isPresent = true;
                    break;
                }
            }
            if (!isPresent)
            {
                m_winningScreen.SetActive(false);
                return;

            }
        }
        m_winningScreen.SetActive(true);
    }
    // Update is called once per frame
    void Update () {
        m_timeElapsed += Time.deltaTime;
        if (m_timeElapsed < CHECK_INTERVAL) return; // don't run this script every frame because it is not necessary
        m_timeElapsed = 0;
        m_detector.checkColliders(hdrCheckWinningCondition);



    }
}
