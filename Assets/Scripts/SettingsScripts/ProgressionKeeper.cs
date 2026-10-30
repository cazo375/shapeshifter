using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
  Music, Background, Wall, Shapes, All
}


public class ProgressionKeeper 
{

  private static int maxThemes = 29;
  private static bool [] musicThemes = new bool[maxThemes];
	private static bool [] backgroundThemes = new bool[maxThemes];
  private static bool [] wallThemes = new bool[maxThemes];
  private static bool [] shapesThemes = new bool[maxThemes];

  private static string outGoingPacket;
  private static string inComingPacket;

  private static string unlockedThemes;
  

	private static ProgressionData progressionData;

	public ProgressionKeeper()
  {
    unlockedThemes = "";
    LoadProgressionData(0);
	}

  private void ResetProgressData()
  {
    musicThemes = new bool[maxThemes];
    backgroundThemes = new bool[maxThemes];
    wallThemes = new bool[maxThemes];
    shapesThemes = new bool[maxThemes];
  }

  public void UnlockAnItem(int index, ItemType type)
  {
    switch (type)
    {
      case ItemType.Music:
        musicThemes[index] = true;
        break;
      case ItemType.Background:
        backgroundThemes[index] = true;
        break;
      case ItemType.Wall:
        wallThemes[index] = true;
        break;
      case ItemType.Shapes:
        shapesThemes[index] = true;
        break;

      default:
        break;
    }
    SaveProgressData(0);
  }

  public void UnlockATheme(int index)
  {
    musicThemes[index] = true;
    backgroundThemes[index] = true;
    wallThemes[index] = true;
    shapesThemes[index] = true;
    
    SaveProgressData(0);
  }

  public void LockEverything()
  {
    Debug.Log("locking" + maxThemes.ToString());
    for(int i = 0; i<maxThemes; i++)
    {
      musicThemes[i] = false;
      backgroundThemes[i] = false;
      wallThemes[i] = false;
      shapesThemes[i] = false;
    }
    SaveProgressData(0);
  }

  public void UnlockEverything()
  {
    Debug.Log("unlocking " + maxThemes.ToString());
    for(int i = 0; i<maxThemes; i++)
    {
      musicThemes[i] = true;
      backgroundThemes[i] = true;
      wallThemes[i] = true;
      shapesThemes[i] = true;
    }
    SaveProgressData(0);
  }

    
	//Loading Functions
	private static void LoadProgressionData(int slot = 0)
  {
		progressionData = new ProgressionData();
        
		progressionData.unlockedThemes = PlayerPrefs.GetString("unlockedThemes" + slot);

		LoadDataFromPacket();
    DecodePacket();
	}

	private static void LoadDataFromPacket()
  {
    Debug.Log("Load Data: " + progressionData.unlockedThemes);
		inComingPacket = progressionData.unlockedThemes;
	}

  private static void DecodePacket()
  {
    char spliter = ',';
    string [] subStrings = inComingPacket.Split(spliter);
    int index = 0;


    foreach (var themeData in subStrings)
    {
      UnlockThemesFromLoad(index, themeData);
      index++;
    }
  }

  private static void UnlockThemesFromLoad(int index, string themeData)
  {
    //Music, Background, Wall, Shapes - 4 bit representations
    //MBWS = 0000 :: 0 for false, 1 for true
    switch (themeData) 
    {
      case "0": //0000
        musicThemes[index] = false;
        backgroundThemes[index] = false;
        wallThemes[index] = false;
        shapesThemes[index] = false;
        break;

      case "1": //0001
        musicThemes[index] = false;
        backgroundThemes[index] = false;
        wallThemes[index] = false;
        shapesThemes[index] = true;
        break;

      case "2": //0010
        musicThemes[index] = false;
        backgroundThemes[index] = false;
        wallThemes[index] = true;
        shapesThemes[index] = false;
        break;

      case "3": //0011
        musicThemes[index] = false;
        backgroundThemes[index] = false;
        wallThemes[index] = true;
        shapesThemes[index] = true;
        break;

      case "4": //0100
        musicThemes[index] = false;
        backgroundThemes[index] = true;
        wallThemes[index] = false;
        shapesThemes[index] = false;
        break;

      case "5": //0101
        musicThemes[index] = false;
        backgroundThemes[index] = true;
        wallThemes[index] = false;
        shapesThemes[index] = true;
        break;

      case "6": //0110
        musicThemes[index] = false;
        backgroundThemes[index] = true;
        wallThemes[index] = true;
        shapesThemes[index] = false;
        break;

      case "7": //0111
        musicThemes[index] = false;
        backgroundThemes[index] = true;
        wallThemes[index] = true;
        shapesThemes[index] = true;
        break;

      case "8": //1000
        musicThemes[index] = true;
        backgroundThemes[index] = false;
        wallThemes[index] = false;
        shapesThemes[index] = false;
        break;

      case "9": //1001
        musicThemes[index] = true;
        backgroundThemes[index] = false;
        wallThemes[index] = false;
        shapesThemes[index] = true;
        break;

      case "10": //1010
        musicThemes[index] = true;
        backgroundThemes[index] = false;
        wallThemes[index] = true;
        shapesThemes[index] = false;
        break;

      case "11": //1011
        musicThemes[index] = true;
        backgroundThemes[index] = false;
        wallThemes[index] = true;
        shapesThemes[index] = true;
        break;

      case "12": //1100
        musicThemes[index] = true;
        backgroundThemes[index] = true;
        wallThemes[index] = false;
        shapesThemes[index] = false;
        break;

      case "13": //1101
        musicThemes[index] = true;
        backgroundThemes[index] = true;
        wallThemes[index] = false;
        shapesThemes[index] = true;
        break;

      case "14": //1110
        musicThemes[index] = true;
        backgroundThemes[index] = true;
        wallThemes[index] = true;
        shapesThemes[index] = false;
        break;

      case "15": //1111
        musicThemes[index] = true;
        backgroundThemes[index] = true;
        wallThemes[index] = true;
        shapesThemes[index] = true;
        break;

      default: //1111 too
        musicThemes[index] = true;
        backgroundThemes[index] = true;
        wallThemes[index] = true;
        shapesThemes[index] = true;
        break;

    }
  }

    //Saving functions
	private static void SaveProgressData(int slot = 0)
  {
		LoadDataToPacket();

		PlayerPrefs.SetString("unlockedThemes" + slot, progressionData.unlockedThemes);

		PlayerPrefs.Save();
	}

	private static void LoadDataToPacket()
  {
		progressionData = new ProgressionData();

		progressionData.unlockedThemes = EncodePacket();

	}

  private static string EncodePacket()
  {
    string spliter = ",";
    string builderString = "";

    for(int i = 0; i<maxThemes; i++)
    {
      builderString = builderString + LoadUnlockedThemesData(i);
      //Debug.Log("LoadUnlockedThemesData: " + LoadUnlockedThemesData(i));
      if((i+1) < maxThemes){
        builderString = builderString + spliter;
      }
    }
    Debug.Log("Save Data: " + builderString);
    unlockedThemes = builderString;
    return builderString;    
  }

  private static int LoadUnlockedThemesData(int index)
  {
    //Debug.Log("ThemesData" + index + ": " + musicThemes[index] + ", " + backgroundThemes[index] + ", " + wallThemes[index] + ", " + shapesThemes[index]);
    // 0000
    if(!musicThemes[index] && !backgroundThemes[index] && !wallThemes[index] && !shapesThemes[index])
    {
      return 0;
    }
    // 0001
    else if(!musicThemes[index] && !backgroundThemes[index] && !wallThemes[index] && shapesThemes[index])
    {
      return 1;
    }
    // 0010
    else if(!musicThemes[index] && !backgroundThemes[index] && wallThemes[index] && !shapesThemes[index])
    {
      return 2;
    }
    // 0011
    else if(!musicThemes[index] && !backgroundThemes[index] && wallThemes[index] && shapesThemes[index])
    {
      return 3;
    }
    // 0100
    else if(!musicThemes[index] && backgroundThemes[index] && !wallThemes[index] && !shapesThemes[index])
    {
      return 4;
    }
    // 0101
    else if(!musicThemes[index] && backgroundThemes[index] && !wallThemes[index] && shapesThemes[index])
    {
      return 5;
    }
    // 0110
    else if(!musicThemes[index] && backgroundThemes[index] && wallThemes[index] && !shapesThemes[index])
    {
      return 6;
    }
    // 0111
    else if(!musicThemes[index] && backgroundThemes[index] && wallThemes[index] && shapesThemes[index])
    {
      return 7;
    }
    // 1000
    else if(musicThemes[index] && !backgroundThemes[index] && !wallThemes[index] && !shapesThemes[index])
    {
      return 8;
    }
    // 1001
    else if(musicThemes[index] && !backgroundThemes[index] && !wallThemes[index] && shapesThemes[index])
    {
      return 9;
    }
    // 1010
    else if(musicThemes[index] && !backgroundThemes[index] && wallThemes[index] && !shapesThemes[index])
    {
      return 10;
    }
    // 1011
    else if(musicThemes[index] && !backgroundThemes[index] && wallThemes[index] && shapesThemes[index])
    {
      return 11;
    }
    // 1100
    else if(musicThemes[index] && backgroundThemes[index] && !wallThemes[index] && !shapesThemes[index])
    {
      return 12;
    }
    // 1101
    else if(musicThemes[index] && backgroundThemes[index] && !wallThemes[index] && shapesThemes[index])
    {
      return 13;
    }
    // 1110
    else if(musicThemes[index] && backgroundThemes[index] && wallThemes[index] && !shapesThemes[index])
    {
      return 14;
    }
    // 1111
    else if(musicThemes[index] && backgroundThemes[index] && wallThemes[index] && shapesThemes[index])
    {
     return 15;
    }
    else
    {
      return 15;
    }
  }


  //Getter functions
  public bool GetMusicUnlocked(int index)
  {
    return musicThemes[index];
  }
  public bool GetBackgroundUnlocked(int index)
  {
    return backgroundThemes[index];
  }
  public bool GetWallUnlocked(int index)
  {
    return wallThemes[index];
  }
  public bool GetShapesUnlocked(int index)
  {
    return shapesThemes[index];
  }

  public List<int> GetUnlockedIndexsOfType(ItemType type)
  {
    List<int> unlockedItems = new List<int>();

    if(type == ItemType.Music)
    {
      for(int i = 0; i<maxThemes; i++)
      {
        if(musicThemes[i])
        {
          unlockedItems.Add(i);
        }
      }
    }
    else if(type == ItemType.Background)
    {
      for(int i = 0; i<maxThemes; i++)
      {
        if(backgroundThemes[i])
        {
          unlockedItems.Add(i);
        }
      }
    }
    else if(type == ItemType.Wall)
    {
      for(int i = 0; i<maxThemes; i++)
      {
        if(wallThemes[i])
        {
          unlockedItems.Add(i);
        }
      }
    }
    else if(type == ItemType.Shapes)
    {
      for(int i = 0; i<maxThemes; i++)
      {
        if(shapesThemes[i])
        {
          unlockedItems.Add(i);
        }
      }
    }

    return unlockedItems;
  }

  public List<int> GetLockedIndexsOfType(ItemType type)
  {
    List<int> lockedItems = new List<int>();

    if(type == ItemType.Music)
    {
      for(int i = 0; i<maxThemes; i++)
      {
        if(!musicThemes[i])
        {
          lockedItems.Add(i);
        }
      }
    }
    else if(type == ItemType.Background)
    {
      for(int i = 0; i<maxThemes; i++)
      {
        if(!backgroundThemes[i])
        {
          lockedItems.Add(i);
        }
      }
    }
    else if(type == ItemType.Wall)
    {
      for(int i = 0; i<maxThemes; i++)
      {
        if(!wallThemes[i])
        {
          lockedItems.Add(i);
        }
      }
    }
    else if(type == ItemType.Shapes){

      for(int i = 0; i<maxThemes; i++)
      {
        if(!shapesThemes[i])
        {
          lockedItems.Add(i);
        }
      }
    }

    return lockedItems;
  }

  public List<int> GetLockedThemeIndexs()
  {
    List<int> lockedThemes = new List<int>();

    for(int i = 0; i<maxThemes; i++)
    {
      if(!backgroundThemes[i] && !musicThemes[i] && !shapesThemes[i] && !wallThemes[i])
      {
        lockedThemes.Add(i);
      }
    }

    return lockedThemes;
  }

  public string GetProgressionString()
  {
    return unlockedThemes;
  }
    
}
