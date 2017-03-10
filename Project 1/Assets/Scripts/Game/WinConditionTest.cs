using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionTest : MonoBehaviour {
    static float CHECK_INTERVAL = 2.0f; //used to check the winning condition each X amount of second
    public ColliderDetector 
        m_detector,m_detectorCover; 
    public List<GameObject> m_objs; // game objects that need to be stored in a box
                                    // Use this for initialization
    public GameObject m_winningScreen;
    float m_timeElapsed = 0;

    bool 
        m_isAllitemsInside  = false,     //True if all items are inside the detector
        m_isAnyItemColliding = true;   //True if any item is colliding with cover

    public GameUIScript gameUI;

    void Start () {
        GameObject gui = GameObject.Find("GameUI");
        gameUI = gui.GetComponent<GameUIScript>();
	}
    void hdrCheckWinningCondition(List<Collider> collider)
    {
        int s = 0; //score temp var

        for (int i = 0; i < m_objs.Count; i++)
        {
            var obj = m_objs[i];
            bool isPresent = false;
            for (int j = 0; j < collider.Count; j++)
            {
                if (collider[j].gameObject == obj)
                {
                    s += 1500;
                    isPresent = true;
                    break;
                }
            }
            var itemInfo = getItemInfo(obj);
            if(itemInfo!= null)
            {
                itemInfo.m_status = (isPresent)? NGame.ITEM_STATUS.REGISTERED: NGame.ITEM_STATUS.UNREGISTERED;
                
            }
            if (!isPresent)
            {
                m_isAllitemsInside = false;
                //m_winningScreen.SetActive(false);
                //return;
            }
        }
        m_isAllitemsInside = true;
        //m_winningScreen.SetActive(true);

        gameUI.score = s;
    }
    NGame.ItemInfo getItemInfo(GameObject obj)
    {
        var itemInfo = obj.GetComponent<NGame.ItemInfo>();
        if (itemInfo != null) return itemInfo;
        if (itemInfo.transform.parent == null) return null;
        return getItemInfo(obj.transform.parent.gameObject);
    }
    void hdrCheckNotWinningCondition(List<Collider> collider)
    {
        m_isAnyItemColliding = collider.Count != 0;


        for (int i = 0; i < m_objs.Count; i++)
        {
            var obj = m_objs[i];
            for (int j = 0; j < collider.Count; j++)
            {
                if (collider[j].gameObject == obj)
                {
                    var itemInfo = getItemInfo(obj);
                    if (itemInfo != null)itemInfo.m_status = NGame.ITEM_STATUS.REGISTERE_ERROR;
                    break;
                    
                }
            }
        }
    }
    // Update is called once per frame
    void Update () {
        if (m_isAllitemsInside && !m_isAnyItemColliding)
        {
            //Winning condition is detected, set the winning condition to true
            m_winningScreen.SetActive(true);
        }else
        {
            //you haven't won the game yet
            m_winningScreen.SetActive(false);
        }
        m_timeElapsed += Time.deltaTime;
        if (m_timeElapsed < CHECK_INTERVAL) return; // don't run this script every frame because it is not necessary
        m_timeElapsed = 0;


        m_detector.checkColliders(hdrCheckWinningCondition);
        m_detectorCover.checkColliders(hdrCheckNotWinningCondition);



    }
}
