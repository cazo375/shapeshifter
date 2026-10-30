using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmWindowController : ButtonClass
{
    private GameController gc;
    public Text themeText;
    public Text typeText;
    public Text priceText;
    public Image itemImage;

    public Button confirmButton;
    public Button cancelButton;

    private int themeIndex;
    private string themeName;
    private ItemType themeType;
    private int cost;
    private GameObject storeButton;


    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        cancelButton.onClick.AddListener(CloseWindow);
	}

    private void CloseWindow()
    {
        gc.CloseConfimWindow();
    }

    public void ItemSetup(int index, string name, int price, ItemType type, GameObject button)
    {
        //set vars
        themeIndex = index;
        themeName = name;
        cost = price;
        themeType = type;
        storeButton = button;

        //set UI
        themeText.text = themeName;
        priceText.text = "Cost: " + cost.ToString();
        // Item Type
        switch (themeType)
        {
            case ItemType.Music:
                typeText.text = "Music";
                break;

            case ItemType.Background:
                typeText.text = "Background";
                break;

            case ItemType.Wall:
                typeText.text = "Wall";
                break;

            case ItemType.Shapes:
                typeText.text = "Shapes";
                break;


            default:
                typeText.text = "NoTypeGiven";
                break;
        }
        
        itemImage.GetComponent<Image>().sprite = GetThemeImage(themeIndex);

        //OnClick
        confirmButton.onClick.AddListener(BuyItem);
    }

    public void ThemeSetup(int index, string theme, int price, GameObject button)
    {
        //set vars
        themeIndex = index;
        themeName = theme;
        cost = price;
        storeButton = button;

        //set UI
        themeText.text = themeName;
        typeText.text = "Whole Theme";
        priceText.text = "Cost: " + cost.ToString();

        itemImage.GetComponent<Image>().sprite = GetThemeImage(themeIndex);

        //OnClick
        confirmButton.onClick.AddListener(BuyTheme);
    }

    private void BuyItem()
    {
		
        if(gc.UnlockAnItem(themeIndex, themeType))
        {
            //Debug.Log ("Theme Unlocked type:" + themeType.ToString() + "  index:" + themeIndex.ToString());
            Destroy(storeButton);
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("No sale, need more funds!");
        }
	}

    private void BuyTheme()
    {
		
        if(gc.UnlockATheme(themeIndex))
        {
            //Debug.Log ("Theme Unlocked index:" + themeIndex.ToString());
            Destroy(storeButton);
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("No sale, need more funds!");
        }
	}

}
