using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InstructionsMenuUI : MonoBehaviour
{
    private GameController gc;
    public GameObject textPanel;
    public GameObject videoPanel;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        ShowTextPanel();
    }

    public void ShowTextPanel()
    {
        textPanel.SetActive(true);
        videoPanel.SetActive(false);
    }

    public void ShowVideoPanel()
    {
        textPanel.SetActive(false);
        videoPanel.SetActive(true);
    }
}
