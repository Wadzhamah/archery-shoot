using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class GlobalVeriables
{
    private static string IS_LINK_AVAILABLE_KEY = "is_available _link";

    public static bool IsLinkAvailable
    {
        get
        {
            return PlayerPrefs.GetInt(IS_LINK_AVAILABLE_KEY, 1) == 1 ? true : false;
        }
        set
        {
            PlayerPrefs.SetInt(IS_LINK_AVAILABLE_KEY, value ? 1 : 0);
        }
    }
}
