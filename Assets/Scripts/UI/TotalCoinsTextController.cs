using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalCoinsTextController : MonoBehaviour
{
    private GameController gc;
    public Text TotalCoinsText;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        TotalCoinsText.text = "Coins: " + gc.scoreKeeper.GetTotalCoins();
    }
}
