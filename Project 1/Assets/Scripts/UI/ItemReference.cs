using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NUI {

    //Reference describing what the item in the world is and what kinds of status it is.
    public enum ItemType {LOTION,CLOCK,SENDWITCH }
    public enum ItemStatus { UNREGISTERED, REGISTERED, REGISTERE_ERROR }
    public class ItemReference : MonoBehaviour
    {
        public ItemType m_type;
        public ItemStatus m_status;

    }

}
