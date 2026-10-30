using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private GameController gc;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    
    }

    public void StartGame()
    {
        gc.RestartGame();
    }

    public void HideMenu()
    {
        gc.HideMenu();
        
    }

    public void LoadHomeMenu()
    {
        gc.SwapToHomeMenu();
    }

    public void LoadInstructionsMenu()
    {
        gc.SwapToInstructionsMenu();
    }

    public void LoadThemesMenu()
    {
        gc.SwapToThemesMenu();
    }

    public void LoadStoreMenu()
    {
        gc.SwapToStoreMenu();
    }

    public void LoadOptionsMenu()
    {
        gc.SwapToOptionsMenu();
    }

    public void LoadCreditsMenu()
    {
        gc.SwapToCreditsMenu();
    }

    public void SetMusicTheme(int selection)
    {
        gc.SetMusicTheme(selection);
    }

    public void SetBackgroundTheme(int selection)
    {
        gc.SetBackgroundTheme(selection);
    }

    public void SetWallTheme(int selection)
    {
        gc.SetWallTheme(selection);
    }

    public void SetShapesTheme(int selection)
    {
        gc.SetShapesTheme(selection);
    }

    public void SaveCurrentThemeSettings()
    { 
        gc.SaveCurrentThemeSettings();
    }

    public void SaveCurrentOptionsSettings()
    { 
        gc.SaveCurrentOptionsSettings();
    }
}
