using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace NGame
{
    
    public class ItemInfo : MonoBehaviour
    {
        [SerializeField]
        ITEM_TYPE m_type;
        public ITEM_STATUS m_status;
        public bool m_isTouchingEdge = false;
        [SerializeField]
        List<ITEM_PURPOSE> m_purposes;
        
        public ITEM_TYPE Type { get { return m_type; } }
        //public ITEM_STATUS Status { get { return m_status; } set { m_status = value; } }
        public List<ITEM_PURPOSE> Purposes { get {
                return m_purposes.Select(item => (ITEM_PURPOSE)item).ToList();
            }
        }

    }

}
