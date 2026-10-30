using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoreThemesButtonController : ButtonClass
{

    private GameController gc;

    public Text buttonText;
    public Text typeText;
    public Text priceText;
    public Text highScoreText;

    public Button thisButton;

    private string themeName;
    private int themeIndex;
    private int cost;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
	}

    public void SetButtonOnClick(int index)
    {
        themeIndex = index;

        SetButtonText();

        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
		gc.CreateThemeConfirmWindow(themeIndex, themeName, cost, gameObject);
	}

    private void SetButtonText()
    {
        string output = GetThemeName(themeIndex);
        //Theme Name
        themeName = output;
        buttonText.text = output;

        // Item Type
        typeText.text = "Whole Theme";

        // Cost
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        cost = gc.GetThemePrice(themeIndex);
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
