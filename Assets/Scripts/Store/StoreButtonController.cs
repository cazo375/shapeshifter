using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreButtonController : ButtonClass
{   
    private GameController gc;

    public Text buttonText;
    public Text typeText;
    public Text priceText;
    public Text highScoreText;
    public Button thisButton;

    private int themeIndex;
    private string themeName;
    private ItemType themeType;
    private int cost;


    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
	}

    public void SetButtonOnClick(int index, ItemType type)
    {
        themeIndex = index;
        themeType = type;
        SetButtonText();

        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
		gc.CreateItemConfirmWindow(themeIndex, themeName, cost, themeType, gameObject);
	}

    private void SetButtonText()
    {
        themeName = GetThemeName(themeIndex);
        buttonText.text = themeName;
        // Cost
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        cost = gc.GetItemPrice(themeIndex, themeType);
        priceText.text = "Cost: " + cost.ToString();

        // Discount
        if(gc.IsDiscountUnlocked(themeIndex))
        {
            highScoreText.text = "Discounted"; 
        }
        else
        {
            highScoreText.text = "Discount @ HS: " + gc.GetThemeDiscountUnlockScores(themeIndex).ToString();
        }
    }

}
