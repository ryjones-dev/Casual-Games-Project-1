using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameSettings {
    public enum GAME_STATE {PLAYING, PAUSED };
    public static GAME_STATE STATE = GAME_STATE.PLAYING;
    public static bool IS_MOUSE_INPUT_INVERTED = false;
    public static float
        MOUSE_SENSITIVITY_ROTATION = 10.0f,
        MOUSE_SENSITIVITY_HORIZONTAL = 10.0f,
        MOUSE_SENSITIVITY_VERTICAL = 10.0f;

}
