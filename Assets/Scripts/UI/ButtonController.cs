using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonController : ButtonClass
{   
    private GameController gc;

    public Text buttonText;
    public Button thisButton;

    private int themeIndex;
    private ItemType themeType;


    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        SetSelected();
	}

    void Update (){
        SetSelected();
    }

    private void SetSelected()
    {
        if(gc.IsItemActive(themeIndex, themeType))
        {
            //Debug.Log("Active item:" + index.ToString());
            thisButton.GetComponent<Animator>().SetBool("Selected", true);
        }
        else
        {
            thisButton.GetComponent<Animator>().SetBool("Normal", true);
            //thisButton.;
            //thisButton.OnSelect(null);
        }
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
		Debug.Log ("Theme Set type:" + themeType.ToString() + "  index:" + themeIndex.ToString());
        gc.SetTheme(themeIndex, themeType);
        //thisButton.Select();
	}

    private void SetButtonText()
    {
        string output = "" + GetThemeName(themeIndex);

        output = output + "\n\n";

        switch (themeType)
        {
            case ItemType.Music:
                output = output + "Music";
                break;

            case ItemType.Background:
                output = output + "Background";
                break;

            case ItemType.Wall:
                output = output + "Wall";
                break;

            case ItemType.Shapes:
                output = output + "Shapes";
                break;


            default:
                output = output + "NoTypeGiven";
                break;
        }

        buttonText.text = output;
    }

}
