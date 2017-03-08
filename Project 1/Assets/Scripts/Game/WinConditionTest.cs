using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionTest : MonoBehaviour {
    static float CHECK_INTERVAL = 2.0f; //used to check the winning condition each X amount of second
    public List<GameObject> m_objs; // game objects that need to be stored in a box
    public ColliderDetector 
        m_detector,m_detectorCover;
                                    // Use this for initialization
    public GameObject m_winningScreen;
    public List<NGame.DEL_GAME_STATUS> m_evntGameWin;
    public string TAG_INTERACTABLE;
    float m_timeElapsed = 0;

    bool 
        m_isAllitemsInside  = false,     //True if all items are inside the detector
        m_isAnyItemColliding = true;   //True if any item is colliding with cover
	void Start () {
        m_evntGameWin = new List<NGame.DEL_GAME_STATUS>();
    }
    void hdrCheckWinningCondition(List<Collider> collider)
    {
        m_isAllitemsInside = true;
        for (int i = 0; i < m_objs.Count; i++)
        {
            var obj = m_objs[i];
            bool isPresent = false;
            for (int j = 0; j < collider.Count; j++)
            {
                if (collider[j].gameObject == obj)
                {
                    isPresent = true;
                    break;
                }
            }
            var itemInfo = getItemInfo(obj);
            //Debug.Log("item I found " + itemInfo);
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
        //m_winningScreen.SetActive(true);
    }
    NGame.ItemInfo getItemInfo(GameObject obj)
    {
        var itemInfo = obj.GetComponent<NGame.ItemInfo>();
        if (itemInfo == null)
        {
            if(obj.transform.parent == null)
                return null;
            return getItemInfo(obj.transform.parent.gameObject);
        }
        return itemInfo;
    }
    void hdrCheckNotWinningCondition(List<Collider> collider)
    {
        m_isAnyItemColliding = false;      
        for (int i = 0; i < m_objs.Count; i++)
        {
            
            var obj = m_objs[i];
            var itemInfo = getItemInfo(obj);
            if(itemInfo != null)
            {
                itemInfo.m_isTouchingEdge = false; ;
            }
            for (int j = 0; j < collider.Count; j++)
            {
                if (collider[j].gameObject == obj)
                {
                    if (m_objs[i].tag == TAG_INTERACTABLE)
                    {
                        //Debug.Log(" I am collding with this upper case " + m_objs[i]);//= false;
                        m_isAnyItemColliding = true;
                    }

                    if (itemInfo != null)
                    {
                        //itemInfo.m_status = NGame.ITEM_STATUS.REGISTERE_ERROR;
                        itemInfo.m_isTouchingEdge = true;
                    }
                    break;
                    
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //cDebug.Log(m_isAllitemsInside + ", " + m_isAnyItemColliding);
        if (m_isAllitemsInside && !m_isAnyItemColliding)
        {
            //Winning condition is detected, set the winning condition to true
            m_winningScreen.SetActive(true);
            for (int i = 0; i < m_evntGameWin.Count; i++) m_evntGameWin[i](true);
        }else
        {
            //you haven't won the game yet
            m_winningScreen.SetActive(false);
            for (int i = 0; i < m_evntGameWin.Count; i++) m_evntGameWin[i](false);
        }
        m_timeElapsed += Time.deltaTime;
        if (m_timeElapsed < CHECK_INTERVAL) return; // don't run this script every frame because it is not necessary
        m_timeElapsed = 0;

       // Debug.Log("I need to get to this point every 1 second");
        //m_isAnyItemColliding = false;
        m_detector.checkColliders(hdrCheckWinningCondition);
        m_detectorCover.checkColliders(hdrCheckNotWinningCondition);



    }
}
