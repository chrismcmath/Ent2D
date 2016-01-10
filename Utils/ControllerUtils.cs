using UnityEngine;
using System.Collections;

namespace Ent2D.Utils {
    public static class ControllerUtils {
        public enum PlayerNumbers { NONE=0, PLAYER1, PLAYER2, PLAYER3, PLAYER4 }

        //NOTE: Keyboard controls for debugging
        public const int KEYBOARD_PLAYER = 1;
        public const string KEYBOARD_ACTION_1_KEY = "space";
        public const string KEYBOARD_ACTION_2_KEY = "f";
        public const string KEYBOARD_HORIZONTAL_AXIS = "Horizontal";
        public const string KEYBOARD_VERTICAL_AXIS = "Vertical";

        public const string JOYSTICK_X_AXIS_FORMAT = "L_XAxis_{0}";
        public const string JOYSTICK_Y_AXIS_FORMAT = "L_YAxis_{1}";
        public const string XBOX_JOYSTICK_FORMAT = "joystick {0} button {1}";
        public const string XBOX_TRIGGER_FORMAT = "TriggersR_{0}";
        public const string XBOX_TRIGGER_FORMAT_OSX = "TriggersR_{0}-OSX";

        //ID REFERENCE: http://wiki.unity3d.com/index.php?title=Xbox360Controller
        public readonly static int[] XBOX_ACTION1_BUTTONS_OSX = new int[] {16, 17, 18, 19};
        public readonly static int[] XBOX_ACTION1_BUTTONS_WIN = new int[] {0, 1, 2, 3};
        public readonly static int[] XBOX_ACTION2_BUTTONS_OSX = new int[] {13, 14};
        public readonly static int[] XBOX_ACTION2_BUTTONS_WIN = new int[] {4, 5};
        public readonly static int[] XBOX_PAUSE_BUTTONS_OSX = new int[] {9};
        public readonly static int[] XBOX_PAUSE_BUTTONS_WIN = new int[] {7};

        public static bool Action1(PlayerNumbers playerNumber) {
            return Action1((int) playerNumber);
        }
        public static bool Action1(int playerID) {
            if (playerID == KEYBOARD_PLAYER) {
                if (Input.GetKey(KEYBOARD_ACTION_1_KEY)) {
                    return true;
                }
            }

            foreach (int buttonID in GetAction1Buttons()) {
                if (Input.GetKey(GetButtonString(playerID, buttonID))) {
                    return true;
                }
            }
            return false;
        }

        public static bool Action1Down(PlayerNumbers playerNumber) {
            return Action1Down((int) playerNumber);
        }
        public static bool Action1Down(int playerID) {
            if (playerID == KEYBOARD_PLAYER) {
                if (Input.GetKeyDown(KEYBOARD_ACTION_1_KEY)) {
                    return true;
                }
            }

            foreach (int buttonID in GetAction1Buttons()) {
                if (Input.GetKeyDown(GetButtonString(playerID, buttonID))) {
                    return true;
                }
            }
            return false;
        }

        public static bool Action1Up(PlayerNumbers playerNumber) {
            return Action1Up((int) playerNumber);
        }
        public static bool Action1Up(int playerID) {
            if (playerID == KEYBOARD_PLAYER) {
                if (Input.GetKeyUp(KEYBOARD_ACTION_1_KEY)) {
                    return true;
                }
            }

            foreach (int buttonID in GetAction1Buttons()) {
                if (Input.GetKeyUp(GetButtonString(playerID, buttonID))) {
                    return true;
                }
            }
            return false;
        }

        public static bool Action2(PlayerNumbers playerNumber) {
            return Action2((int) playerNumber);
        }
        public static bool Action2(int playerID) {
            if (playerID == KEYBOARD_PLAYER) {
                if (Input.GetKey(KEYBOARD_ACTION_2_KEY)) {
                    return true;
                }
            }

            foreach (int buttonID in GetAction2Buttons()) {
                if (Input.GetKey(GetButtonString(playerID, buttonID))) {
                    return true;
                }
            }
            return false;
        }

        public static bool Action2Down(PlayerNumbers playerNumber) {
            return Action2Down((int) playerNumber);
        }
        public static bool Action2Down(int playerID) {
            if (playerID == KEYBOARD_PLAYER) {
                if (Input.GetKeyDown(KEYBOARD_ACTION_2_KEY)) {
                    return true;
                }
            }

            foreach (int buttonID in GetAction2Buttons()) {
                if (Input.GetKeyDown(GetButtonString(playerID, buttonID))) {
                    return true;
                }
            }
            return false;
        }

        public static bool Action2Up(PlayerNumbers playerNumber) {
            return Action2Up((int) playerNumber);
        }
        public static bool Action2Up(int playerID) {
            if (playerID == KEYBOARD_PLAYER) {
                if (Input.GetKeyUp(KEYBOARD_ACTION_2_KEY)) {
                    return true;
                }
            }

            foreach (int buttonID in GetAction2Buttons()) {
                if (Input.GetKeyUp(GetButtonString(playerID, buttonID))) {
                    return true;
                }
            }
            return false;
        }

        public static bool PauseDown(PlayerNumbers playerNumber) {
            return PauseDown((int) playerNumber);
        }
        public static bool PauseDown(int playerID) {
            foreach (int buttonID in GetPauseButtons()) {
                if (Input.GetKeyDown(GetButtonString(playerID, buttonID))) {
                    return true;
                }
            }
            return false;
        }

        public static float LeftStickHorizontal(PlayerNumbers playerNumber) {
            return LeftStickHorizontal((int) playerNumber);
        }
        public static float LeftStickHorizontal(int playerID) {
            if (playerID == KEYBOARD_PLAYER) {
                return Input.GetAxis(KEYBOARD_HORIZONTAL_AXIS);
            }

            return Input.GetAxis(string.Format(JOYSTICK_X_AXIS_FORMAT, playerID.ToString()));
        }

        public static float LeftStickVertical(PlayerNumbers playerNumber) {
            return LeftStickVertical((int) playerNumber);
        }
        public static float LeftStickVertical(int playerID) {
            if (playerID == KEYBOARD_PLAYER) {
                return Input.GetAxis(KEYBOARD_VERTICAL_AXIS);
            }

            return Input.GetAxis(string.Format(JOYSTICK_Y_AXIS_FORMAT, playerID.ToString()));
        }

        private static int[] GetAction1Buttons() {
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            return XBOX_ACTION1_BUTTONS_OSX;
#else
            return XBOX_ACTION1_BUTTONS_WIN;
#endif
        }

        private static int[] GetAction2Buttons() {
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            return XBOX_ACTION2_BUTTONS_OSX;
#else
            return XBOX_ACTION2_BUTTONS_WIN;
#endif
        }

        private static int[] GetPauseButtons() {
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            return XBOX_PAUSE_BUTTONS_OSX;
#else
            return XBOX_PAUSE_BUTTONS_WIN;
#endif
        }

        private static string GetButtonString(int playerID, int buttonID) {
            return string.Format(XBOX_JOYSTICK_FORMAT, playerID, buttonID);
        }
    }
}
