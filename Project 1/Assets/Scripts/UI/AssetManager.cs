using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NUI
{
    //Sprite bank for UI
    public class AssetManager : MonoBehaviour
    {
        public static AssetManager ME;
        public Sprite SPRT_ERROR;
        public Sprite
            SPRT_LOTION,
            SPRT_CLOCK,
            SPRT_SENDWITCH,
            SPRT_REGISTERED,
            SPRT_UNREGISTERED,
            SPRT_REGISTERED_ERROR;

        void Awake() {
            ME = this;
        }
    }

}
