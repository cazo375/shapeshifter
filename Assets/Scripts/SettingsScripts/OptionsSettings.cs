using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSettings 
{
    private static int musicVolumeSelected;
    private static int effectsVolumeSelected;
    private static OptionsSettingsData optionsSettingsData;


    public OptionsSettings()
    {
        musicVolumeSelected = 50;
        effectsVolumeSelected = 50;

        LoadOptionsSettingsData();
    }

    //Saving Functions
	private static void LoadOptionsSettingsData(int slot = 0){
		optionsSettingsData = new OptionsSettingsData();
        optionsSettingsData.musicVolume = PlayerPrefs.GetInt("musicVolume" + slot);
        optionsSettingsData.effectsVolume = PlayerPrefs.GetInt("effectsVolume" + slot);

		LoadDataFromPacket();
	}

	private static void LoadDataFromPacket()
    {
        musicVolumeSelected = optionsSettingsData.musicVolume; 
        effectsVolumeSelected = optionsSettingsData.effectsVolume;
	}

	private static void SaveOptionsSettingsData(int slot = 0)
    {
		LoadDataToPacket();
        PlayerPrefs.SetInt("musicVolume" + slot, optionsSettingsData.musicVolume);
        PlayerPrefs.SetInt("effectsVolume" + slot, optionsSettingsData.effectsVolume);

		PlayerPrefs.Save();
	}

	private static void LoadDataToPacket()
    {
		optionsSettingsData = new OptionsSettingsData();
        optionsSettingsData.musicVolume = musicVolumeSelected;
        optionsSettingsData.effectsVolume = effectsVolumeSelected;
	}

    public void SaveCurrentOptionsSettings()
    {
        SaveOptionsSettingsData();
    }


    public void SetMusicVolume(float musicVolumeSelection)
    {
        musicVolumeSelected = (int)(musicVolumeSelection * 100);
    }

    public void SetEffectsVolume(float effectsVolumeSelection)
    {
        effectsVolumeSelected = (int)(effectsVolumeSelection * 100);
    }


    public float GetMusicVolume()
    {
        return (float)((float)musicVolumeSelected / 100.0f);
    }

    public float GetEffectsVolume()
    {
        return (float)((float)effectsVolumeSelected / 100.0f);
    }
}
