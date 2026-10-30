using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemesSelectionButtonLoaderUI : MonoBehaviour
{
    private GameController gc;

    public GameObject buttonPrefab;
    
    public ItemType themeType;

    public GameObject contentHolder;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();

        List<int> unlockedItems = gc.progressionKeeper.GetUnlockedIndexsOfType(themeType);
        foreach (int index in unlockedItems)
        {
            CreateButton(index, themeType);
        }

    }

    private void CreateButton(int index, ItemType type)
    {
        GameObject newButton = Instantiate(buttonPrefab,contentHolder.transform);
        newButton.GetComponent<ButtonController>().SetButtonOnClick(index, type);
    }
}
