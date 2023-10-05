using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    public static bool boolSoundOn = true;
    public Image soundImage;
    public Sprite soundOn;
    public Sprite soundOff;

    SoundManager soundManager;


    void Start()
    {
        soundManager = SoundManager.Instance;
        setSound(PlayerPrefs.GetInt("soundOn", 1) == 1, false);
        boolSoundOn = (Convert.ToBoolean(PlayerPrefs.GetInt("soundOn")));

        //AudioListener.volume = 0;
    }



    public void TurnSound()
    {

        setSound(!boolSoundOn, false);

    }

    private void setSound(bool sound, bool playSfx)
    {
        if (playSfx)
        {
            SoundManager.PlaySfx(soundManager.soundClick, 1f);
        }
        boolSoundOn = !boolSoundOn;

        if (sound)
        {
            soundImage.sprite = soundOn;
            PlayerPrefs.SetInt("soundOn", 1);
            SoundManager.MusicVolume = 1;
            SoundManager.SoundVolume = 1;
            //SoundManager.PlaySfx(soundManager.soundClick);
        }

        else
        {
            SoundManager.PlaySfx(soundManager.soundClick, 1f);
            soundImage.sprite = soundOff;
            PlayerPrefs.SetInt("soundOn", 0);
            SoundManager.MusicVolume = 0;
            SoundManager.SoundVolume = 0;
        }

       // SoundManager.PlaySfx(soundManager.soundClick, 1f);
    }
}
