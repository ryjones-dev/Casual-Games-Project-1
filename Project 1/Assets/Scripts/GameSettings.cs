using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameSettings {
    public enum GAME_STATE {PLAYING, PAUSED, FROZEN };
    public static GAME_STATE STATE = GAME_STATE.PLAYING;
    public static bool IS_MOUSE_INPUT_INVERTED = false;
    public static float MOUSE_SENSITIVITY = 10.0f;

}
