﻿using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public class KeyEvent
    {


        private readonly int keyCode;

        public KeyEvent(int keyCode)
        {
            this.keyCode = keyCode;
        }

        public int getKeyCode()
        {
            return keyCode;
        }

        public const int VK_LEFTBUTTON = 0x01;
        public const int VK_RIGHTBUTTON = 0x02;
        public const int VK_CANCEL = 0x03;
        public const int VK_MIDDLEBUTTON = 0x04;
        public const int VK_EXTRABUTTON1 = 0x05;
        public const int VK_EXTRABUTTON2 = 0x06;
        public const int VK_BACK = 0x08;
        public const int VK_TAB = (int)Keys.Tab;
        public const int VK_CLEAR = 0x0C;
        public const int VK_RETURN = 0x0D;
        public const int VK_ENTER = VK_RETURN;
        public const int VK_SHIFT = 0x10;
        public const int VK_CONTROL = 0x11;
        public const int VK_MENU = 0x12;
        public const int VK_ALT = VK_MENU;
        public const int VK_ALT_GRAPH = 0xFF7E;
        public const int VK_PAUSE = 0x13;
        public const int VK_CAPSLOCK = 0x14;
        public const int VK_KANA = 0x15;
        public const int VK_HANGEUL = 0x15;
        public const int VK_HANGUL = 0x15;
        public const int VK_JUNJA = 0x17;
        public const int VK_FINAL = 0x18;
        public const int VK_HANJA = 0x19;
        public const int VK_KANJI = 0x19;
        public const int VK_ESCAPE = 0x1B;
        public const int VK_CONVERT = 0x1C;
        public const int VK_NONCONVERT = 0x1D;
        public const int VK_ACCEPT = 0x1E;
        public const int VK_MODECHANGE = 0x1F;
        public const int VK_SPACE = 0x20;
        public const int VK_PRIOR = 0x21;
        public const int VK_NEXT = 0x22;
        public const int VK_END = 0x23;
        public const int VK_HOME = 0x24;
        public const int VK_LEFT = (int)Keys.Left;
        public const int VK_UP = (int)Keys.Up;
        public const int VK_RIGHT = (int)Keys.Right;
        public const int VK_DOWN = (int)Keys.Down;
        public const int VK_SELECT = 0x29;
        public const int VK_PRINT = 0x2A;
        public const int VK_EXECUTE = 0x2B;
        public const int VK_SNAPSHOT = 0x2C;
        public const int VK_INSERT = 0x2D;
        public const int VK_DELETE = 0x2E;
        public const int VK_HELP = 0x2F;
        public const int VK_N0 = 0x30;
        public const int VK_N1 = 0x31;
        public const int VK_N2 = 0x32;
        public const int VK_N3 = 0x33;
        public const int VK_N4 = 0x34;
        public const int VK_N5 = 0x35;
        public const int VK_N6 = 0x36;
        public const int VK_N7 = 0x37;
        public const int VK_N8 = 0x38;
        public const int VK_N9 = 0x39;
        public const int VK_A = 0x41;
        public const int VK_B = 0x42;
        public const int VK_C = 0x43;
        public const int VK_D = 0x44;
        public const int VK_E = 0x45;
        public const int VK_F = 0x46;
        public const int VK_G = 0x47;
        public const int VK_H = 0x48;
        public const int VK_I = 0x49;
        public const int VK_J = 0x4A;
        public const int VK_K = 0x4B;
        public const int VK_L = 0x4C;
        public const int VK_M = 0x4D;
        public const int VK_N = 0x4E;
        public const int VK_O = 0x4F;
        public const int VK_P = 0x50;
        public const int VK_Q = 0x51;
        public const int VK_R = 0x52;
        public const int VK_S = 0x53;
        public const int VK_T = 0x54;
        public const int VK_U = 0x55;
        public const int VK_V = 0x56;
        public const int VK_W = 0x57;
        public const int VK_X = 0x58;
        public const int VK_Y = 0x59;
        public const int VK_Z = 0x5A;
        public const int VK_LEFTWINDOWS = 0x5B;
        public const int VK_RIGHTWINDOWS = 0x5C;
        public const int VK_APPLICATION = 0x5D;
        public const int VK_SLEEP = 0x5F;
        public const int VK_NUMPAD0 = 0x60;
        public const int VK_NUMPAD1 = 0x61;
        public const int VK_NUMPAD2 = 0x62;
        public const int VK_NUMPAD3 = 0x63;
        public const int VK_NUMPAD4 = 0x64;
        public const int VK_NUMPAD5 = 0x65;
        public const int VK_NUMPAD6 = 0x66;
        public const int VK_NUMPAD7 = 0x67;
        public const int VK_NUMPAD8 = 0x68;
        public const int VK_NUMPAD9 = 0x69;
        public const int VK_MULTIPLY = 0x6A;
        public const int VK_ADD = 0x6B;
        public const int VK_SEPARATOR = 0x6C;
        public const int VK_SUBTRACT = 0x6D;
        public const int VK_DECIMAL = 0x6E;
        public const int VK_DIVIDE = 0x6F;
        public const int VK_F1 = 0x70;
        public const int VK_F2 = 0x71;
        public const int VK_F3 = 0x72;
        public const int VK_F4 = 0x73;
        public const int VK_F5 = 0x74;
        public const int VK_F6 = 0x75;
        public const int VK_F7 = 0x76;
        public const int VK_F8 = 0x77;
        public const int VK_F9 = 0x78;
        public const int VK_F10 = 0x79;
        public const int VK_F11 = 0x7A;
        public const int VK_F12 = 0x7B;
        public const int VK_F13 = 0x7C;
        public const int VK_F14 = 0x7D;
        public const int VK_F15 = 0x7E;
        public const int VK_F16 = 0x7F;
        public const int VK_F17 = 0x80;
        public const int VK_F18 = 0x81;
        public const int VK_F19 = 0x82;
        public const int VK_F20 = 0x83;
        public const int VK_F21 = 0x84;
        public const int VK_F22 = 0x85;
        public const int VK_F23 = 0x86;
        public const int VK_F24 = 0x87;
        public const int VK_NUMLOCK = 0x90;
        public const int VK_SCROLLLOCK = 0x91;
        public const int VK_NEC_EQUAL = 0x92;
        public const int VK_FUJITSU_JISHO = 0x92;
        public const int VK_FUJITSU_MASSHOU = 0x93;
        public const int VK_FUJITSU_TOUROKU = 0x94;
        public const int VK_FUJITSU_LOYA = 0x95;
        public const int VK_FUJITSU_ROYA = 0x96;
        public const int VK_LEFTSHIFT = 0xA0;
        public const int VK_RIGHTSHIFT = 0xA1;
        public const int VK_LEFTCONTROL = 0xA2;
        public const int VK_RIGHTCONTROL = 0xA3;
        public const int VK_LEFTMENU = 0xA4;
        public const int VK_RIGHTMENU = 0xA5;
        public const int VK_BROWSERBACK = 0xA6;
        public const int VK_BROWSERFORWARD = 0xA7;
        public const int VK_BROWSERREFRESH = 0xA8;
        public const int VK_BROWSERSTOP = 0xA9;
        public const int VK_BROWSERSEARCH = 0xAA;
        public const int VK_BROWSERFAVORITES = 0xAB;
        public const int VK_BROWSERHOME = 0xAC;
        public const int VK_VOLUMEMUTE = 0xAD;
        public const int VK_VOLUMEDOWN = 0xAE;
        public const int VK_VOLUMEUP = 0xAF;
        public const int VK_MEDIANEXTTRACK = 0xB0;
        public const int VK_MEDIAPREVTRACK = 0xB1;
        public const int VK_MEDIASTOP = 0xB2;
        public const int VK_MEDIAPLAYPAUSE = 0xB3;
        public const int VK_LAUNCHMAIL = 0xB4;
        public const int VK_LAUNCHMEDIASELECT = 0xB5;
        public const int VK_LAUNCHAPPLICATION1 = 0xB6;
        public const int VK_LAUNCHAPPLICATION2 = 0xB7;
        public const int VK_OEM1 = 0xBA;
        public const int VK_OEMPLUS = 0xBB;
        public const int VK_OEMCOMMA = 0xBC;
        public const int VK_OEMMINUS = 0xBD;
        public const int VK_OEMPERIOD = 0xBE;
        public const int VK_OEM2 = 0xBF;
        public const int VK_OEM3 = 0xC0;
        public const int VK_OEM4 = 0xDB;
        public const int VK_OEM5 = 0xDC;
        public const int VK_OEM6 = 0xDD;
        public const int VK_OEM7 = 0xDE;
        public const int VK_OEM8 = 0xDF;
        public const int VK_OEMAX = 0xE1;
        public const int VK_OEM102 = 0xE2;
        public const int VK_ICOHELP = 0xE3;
        public const int VK_ICO00 = 0xE4;
        public const int VK_PROCESSKEY = 0xE5;
        public const int VK_ICOCLEAR = 0xE6;
        public const int VK_PACKET = 0xE7;
        public const int VK_OEMRESET = 0xE9;
        public const int VK_OEMJUMP = 0xEA;
        public const int VK_OEMPA1 = 0xEB;
        public const int VK_OEMPA2 = 0xEC;
        public const int VK_OEMPA3 = 0xED;
        public const int VK_OEMWSCTRL = 0xEE;
        public const int VK_OEMCUSEL = 0xEF;
        public const int VK_OEMATTN = 0xF0;
        public const int VK_OEMFINISH = 0xF1;
        public const int VK_OEMCOPY = 0xF2;
        public const int VK_OEMAUTO = 0xF3;
        public const int VK_OEMENLW = 0xF4;
        public const int VK_OEMBACKTAB = 0xF5;
        public const int VK_ATTN = 0xF6;
        public const int VK_CRSEL = 0xF7;
        public const int VK_EXSEL = 0xF8;
        public const int VK_EREOF = 0xF9;
        public const int VK_PLAY = 0xFA;
        public const int VK_ZOOM = 0xFB;
        public const int VK_NONAME = 0xFC;
        public const int VK_PA1 = 0xFD;
        public const int VK_OEMCLEAR = 0xFE;
    }
}
