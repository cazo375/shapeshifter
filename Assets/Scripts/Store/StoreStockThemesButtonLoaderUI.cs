using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreStockThemesButtonLoaderUI : MonoBehaviour
{
    private GameController gc;

    public GameObject buttonPrefab;
    
    public GameObject contentHolder;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();

        List<int> unlockedItems = gc.progressionKeeper.GetLockedThemeIndexs();
        foreach (int index in unlockedItems)
        {
            CreateButton(index);
        }

    }

    private void CreateButton(int index)
    {
        GameObject newButton = Instantiate(buttonPrefab,contentHolder.transform);
        newButton.GetComponent<StoreThemesButtonController>().SetButtonOnClick(index);
    }
}
