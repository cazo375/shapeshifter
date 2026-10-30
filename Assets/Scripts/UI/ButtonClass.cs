using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClass : MonoBehaviour
{
    public string GetThemeName(int themeIndex)
    {
         switch (themeIndex)
        {
            case 0:
                return "Spider";

            case 1:
                return "Hacker";

            case 2:
                return "PurpGorilla";

            case 3:
                return "Atlas";

            case 4:
                return "Blkmosaic";

            case 5:
                return "Club";

            case 6:
                return "Green";

            case 7:
                return "PuNKrOCK";

            case 8:
                return "SadWall";

            case 9:
                return "Simple Stick Combat";

            case 10:
                return "SynthWave";

            case 11:
                return "Blue";

            case 12:
                return "Cells Interlinked";

            case 13:
                return "Impossible";

            case 14:
                return "Bubby";

            case 15:
                return "Keys";

            case 16:
                return "Mf";

            case 17:
                return "Thinking";

            case 18:
                return "Underwater";

            case 19:
                return "Pug";

            case 20:
                return "Ramen";

            case 21:
                return "Space1";

            case 22:
                return "DontSeizure";

            case 23:
                return "MesmerBlue";
            
            case 24:
                return "Orange";

            case 25:
                return "Red";

            case 26:
                return "PurpleChill";

            case 27:
                return "Yellow";

            case 28:
                return "Noir";
            
            default:
                return "NoNameGiven";
        }
    }


    public Sprite GetThemeImage(int index){
        switch (index)
        {
            case 0:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/SpiderThemeImage");

            case 1:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/HackerThemeImage");

            case 2:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/PurpGorillaThemeImage");

            case 3:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/AtlasThemeImage");
                
            case 4:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/BlkmosaicThemeImage");

            case 5:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/ClubThemeImage");
                
            case 6:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/GreenThemeImage");
    
            case 7:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/PuNKrOCKThemeImage");
                    
            case 8:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/SadWallThemeImage");
                    
            case 9:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/SimpleStickCombatThemeImage");
                    
            case 10:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/SynthWaveThemeImage");
                    
            case 11:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/BlueThemeImage");
                    
            case 12:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/CellsInterLinkedThemeImage");
                    
            case 13:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/ImpossibleThemeImage");
                    
            case 14:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/BubbyThemeImage");
                    
            case 15:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/KeysThemeImage");
                    
            case 16:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/MfThemeImage");

            case 17:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/ThinkingThemeImage");
                    
            case 18:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/UnderwaterThemeImage");
                
            case 19:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/PugThemeImage");
                    
            case 20:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/RamenThemeImage");
                
            case 21:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/Space1ThemeImage");
                
            case 22:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/DontSeizureThemeImage");
               
            case 23:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/MesmerBlueThemeImage");
                
            case 24:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/OrangeThemeImage");
                
            case 25:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/RedThemeImage");
                
            case 26:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/PurpleChillThemeImage");
                 
            case 27:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/YellowThemeImage");
                
            case 28:
                return Resources.Load<Sprite>("StoreImages/ThemeImages/NoirThemeImage");
            default:
                return null;
        }
    }
}
