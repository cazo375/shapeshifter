using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreMenuController : MonoBehaviour
{
    public GameObject selectionPanel;
    public GameObject byItemsPanel;
    public GameObject byThemesPanel;


    void Start()
    {
        selectionPanel.SetActive(true);
        byItemsPanel.SetActive(false);
        byThemesPanel.SetActive(false);
    }

    public void SwapToByItemsPanel()
    {
        selectionPanel.SetActive(false);
        byItemsPanel.SetActive(true);
    }

    public void SwapToByThemesPanel()
    {
        selectionPanel.SetActive(false);
        byThemesPanel.SetActive(true);
    }
}
