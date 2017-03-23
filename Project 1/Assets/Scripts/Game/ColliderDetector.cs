using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class ColliderDetector : MonoBehaviour {
    public delegate void DEL_COLLIDER(List<Collider> collider);
    int m_checkCycle = 0;
    List<DEL_COLLIDER> m_evntCollideds = new List<DEL_COLLIDER>();
    List<Collider> m_colliders = new List<Collider>();

    public int ItemCount { get { return m_evntCollideds.Count; } }

    // Use this for initialization
    void Start () {
        var collider = GetComponent<Collider>();
        collider.isTrigger = true;
        

	}
    
	
    public void checkColliders(DEL_COLLIDER evntCollided)
    {
        if(m_checkCycle==0)m_checkCycle = 2;
        m_evntCollideds.Add(evntCollided);
    }


    void OnTriggerStay(Collider other)
    {
        if (m_checkCycle != 1) return;
        m_colliders.Add(other);
    }
    void FixedUpdate()
    {
        switch (m_checkCycle)
        {
            case 0:
                return;
            case 1:
                foreach (var e in m_evntCollideds)
                    e(m_colliders);
                m_evntCollideds.Clear();
                m_colliders.Clear();
                break;
            case 2:
                //The cycle is initiated let it run                
                break;
        }
        m_checkCycle--;


    }

    /*
     * Remove the comment bracket to test out how it works
     *  void testFunc(List<Collider> colliders)
    {
        if (colliders.Count == 0) return;
        var text =  colliders.Select(s => s.gameObject.name).ToArray<string>();
        Debug.Log("Collided : " + string.Join(",",text));
    }
    void Update()
    {
        checkColliders(testFunc);

    }
     * */

}
