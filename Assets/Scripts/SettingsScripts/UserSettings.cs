using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettings
{
    //Selected themes
    private static int musicThemeSelected;
    private static int backgroundThemeSelected;
    private static int wallThemeSelected;
    private static int shapeThemeSelected;

    private static UserSettingsData userSettingsData;

    public UserSettings()
    {
        musicThemeSelected = 0;
        backgroundThemeSelected = 0;
        wallThemeSelected = 0;
        shapeThemeSelected = 0;

        LoadUserSettingsData();
    }

    //Saving Functions
	private static void LoadUserSettingsData(int slot = 0)
    {
		userSettingsData = new UserSettingsData();
        
		userSettingsData.lastMusicTheme = PlayerPrefs.GetInt("lastMusicTheme" + slot);
        userSettingsData.lastBackgroundTheme = PlayerPrefs.GetInt("lastBackgroundTheme" + slot);
        userSettingsData.lastWallTheme = PlayerPrefs.GetInt("lastWallTheme" + slot);
        userSettingsData.lastShapeTheme = PlayerPrefs.GetInt("lastShapeTheme" + slot);

		LoadDataFromPacket();
	}

	private static void LoadDataFromPacket()
    {
		musicThemeSelected = userSettingsData.lastMusicTheme;
        backgroundThemeSelected = userSettingsData.lastBackgroundTheme;
        wallThemeSelected = userSettingsData.lastWallTheme;
        shapeThemeSelected = userSettingsData.lastShapeTheme; 
	}

	private static void SaveUserSettingsData(int slot = 0)
    {
		LoadDataToPacket();

		PlayerPrefs.SetInt("lastMusicTheme" + slot, userSettingsData.lastMusicTheme);
        PlayerPrefs.SetInt("lastBackgroundTheme" + slot, userSettingsData.lastBackgroundTheme);
        PlayerPrefs.SetInt("lastWallTheme" + slot, userSettingsData.lastWallTheme);
        PlayerPrefs.SetInt("lastShapeTheme" + slot, userSettingsData.lastShapeTheme);

		PlayerPrefs.Save();
	}

	private static void LoadDataToPacket()
    {
		userSettingsData = new UserSettingsData();
		userSettingsData.lastMusicTheme = musicThemeSelected;
        userSettingsData.lastBackgroundTheme = backgroundThemeSelected;
        userSettingsData.lastWallTheme = wallThemeSelected;
        userSettingsData.lastShapeTheme = shapeThemeSelected;
	}

    public void SaveCurrentThemeSettings()
    {
        SaveUserSettingsData();
    }


    public void SetThemeForAll(int newTheme)
    {
        musicThemeSelected = newTheme;
        backgroundThemeSelected = newTheme;
        wallThemeSelected = newTheme;
        shapeThemeSelected = newTheme;

        SaveUserSettingsData();
    }

    public void SetMusicTheme(int musicSelection) 
    { 
        musicThemeSelected = musicSelection; 
    }
    public void SetBackgroundTheme(int backgroundSelection) 
    { 
        backgroundThemeSelected = backgroundSelection; 
    }
    public void SetWallTheme(int wallSelection) 
    { 
        wallThemeSelected = wallSelection; 
    }
    public void SetShapeTheme(int shapeSelection) 
    { 
        shapeThemeSelected = shapeSelection; 
    }


    public int GetSelectedMusicTheme() 
    { 
        return musicThemeSelected; 
    }
    public int GetSelectedBackgroundTheme()
    { 
        return backgroundThemeSelected; 
    }
    public int GetSelectedWallTheme() 
    { 
        return wallThemeSelected; 
    }
    public int GetSelectedShapesTheme() 
    { 
        return shapeThemeSelected; 
    }
}
