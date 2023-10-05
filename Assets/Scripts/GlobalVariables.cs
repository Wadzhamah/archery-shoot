using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    public static string INFO_URL = "https://www.youtube.com/";
    private static string USER_NAME_KEY = "username";

    public static string UserName
    {
        get
        {
            return PlayerPrefs.GetString(USER_NAME_KEY, "USERNAME");
        }
        set
        {
            PlayerPrefs.SetString(USER_NAME_KEY, value);
        }
    }
}
