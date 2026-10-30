using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreKeeper 
{
    private static int maxThemes = 29;
    private static int [] musicThemes = new int[maxThemes];
    private static int [] backgroundThemes = new int[maxThemes];
    private static int [] wallThemes = new int[maxThemes];
    private static int [] shapesThemes = new int[maxThemes];

    private static int [] discountUnlockScores = new int[maxThemes];

    public StoreKeeper()
    {
        SetDefaultPriceList();
    }

    public int GetItemPrice(int index, ItemType type, int highScore)
    {
        int cost = 0;
        //Debug.Log("Get Price: " + index.ToString() + ", Type: " + type.ToString());
        switch (type)
        {
            case ItemType.Music:
                cost = musicThemes[index];
                break;

            case ItemType.Background:
                cost = backgroundThemes[index];
                break;

            case ItemType.Wall:
                cost = wallThemes[index];
                break;

            case ItemType.Shapes:
                cost = shapesThemes[index];
                break;


            default:
            break;
        }

        if(IsDiscountUnlocked(index, highScore)){
            return (cost / 2);
        }

        //Debug.Log("Item Cost: " + cost.ToString());
        return cost;
    }


    public int GetThemePrice(int index, int highScore)
    {
        int cost = 0;
        cost += musicThemes[index];
        cost += backgroundThemes[index];
        cost += wallThemes[index];
        cost += shapesThemes[index];

        if(IsDiscountUnlocked(index, highScore)){
            cost = (cost / 2);
        }

        float groupDiscount = (float)cost * 0.1f;
        cost = cost - (int)groupDiscount;
        
        //Debug.Log("Group Cost: " + cost.ToString());
        return cost;
    }

    public int GetThemeCostPreDiscount(int index)
    {
        int cost = 0;
        cost += musicThemes[index];
        cost += backgroundThemes[index];
        cost += wallThemes[index];
        cost += shapesThemes[index];

        float groupDiscount = (float)cost * 0.1f;

        cost = cost - (int)groupDiscount;
        return cost;
    }

    public int GetThemeDiscountUnlockScores(int index)
    {
        return discountUnlockScores[index];
    }

    public bool IsDiscountUnlocked(int index, int highScore)
    {
        if(highScore >= discountUnlockScores[index])
        {
            //Debug.Log("Unlocked!");
            return true;
        }
        //Debug.Log("Locked!");
        return false;
    }

    public void SetDefaultPriceList()
    {
        musicThemes[0] = 40;    backgroundThemes[0] = 60;    wallThemes[0] = 40;      shapesThemes[0] = 60;     discountUnlockScores[0] = 75;
        musicThemes[1] = 40;    backgroundThemes[1] = 80;    wallThemes[1] = 40;      shapesThemes[1] = 60;     discountUnlockScores[1] = 100;
        musicThemes[2] = 40;    backgroundThemes[2] = 40;    wallThemes[2] = 40;      shapesThemes[2] = 40;     discountUnlockScores[2] = 25;
        musicThemes[3] = 40;    backgroundThemes[3] = 40;    wallThemes[3] = 60;      shapesThemes[3] = 40;     discountUnlockScores[3] = 50;
        musicThemes[4] = 40;    backgroundThemes[4] = 40;    wallThemes[4] = 40;      shapesThemes[4] = 40;     discountUnlockScores[4] = 25;
        musicThemes[5] = 40;    backgroundThemes[5] = 40;    wallThemes[5] = 60;      shapesThemes[5] = 40;     discountUnlockScores[5] = 50;
        musicThemes[6] = 40;    backgroundThemes[6] = 40;    wallThemes[6] = 40;      shapesThemes[6] = 40;     discountUnlockScores[6] = 25;     
        musicThemes[7] = 40;    backgroundThemes[7] = 40;    wallThemes[7] = 60;      shapesThemes[7] = 40;     discountUnlockScores[7] = 50;
        musicThemes[8] = 40;    backgroundThemes[8] = 40;    wallThemes[8] = 40;      shapesThemes[8] = 40;     discountUnlockScores[8] = 25;
        musicThemes[9] = 40;    backgroundThemes[9] = 40;    wallThemes[9] = 60;      shapesThemes[9] = 40;     discountUnlockScores[9] = 50;
        musicThemes[10] = 40;   backgroundThemes[10] = 40;   wallThemes[10] = 40;     shapesThemes[10] = 40;    discountUnlockScores[10] = 25;
        musicThemes[11] = 40;   backgroundThemes[11] = 40;   wallThemes[11] = 60;     shapesThemes[11] = 40;    discountUnlockScores[11] = 50;
        musicThemes[12] = 40;   backgroundThemes[12] = 40;   wallThemes[12] = 40;     shapesThemes[12] = 40;    discountUnlockScores[12] = 25;
        musicThemes[13] = 40;   backgroundThemes[13] = 60;   wallThemes[13] = 80;     shapesThemes[13] = 40;    discountUnlockScores[13] = 100;
        musicThemes[14] = 40;   backgroundThemes[14] = 40;   wallThemes[14] = 40;     shapesThemes[14] = 40;    discountUnlockScores[14] = 25;
        musicThemes[15] = 40;   backgroundThemes[15] = 40;   wallThemes[15] = 40;     shapesThemes[15] = 40;    discountUnlockScores[15] = 25;
        musicThemes[16] = 40;   backgroundThemes[16] = 40;   wallThemes[16] = 40;     shapesThemes[16] = 40;    discountUnlockScores[16] = 25;
        musicThemes[17] = 40;   backgroundThemes[17] = 40;   wallThemes[17] = 40;     shapesThemes[17] = 40;    discountUnlockScores[17] = 25;
        musicThemes[18] = 40;   backgroundThemes[18] = 40;   wallThemes[18] = 60;     shapesThemes[18] = 60;    discountUnlockScores[18] = 75;
        musicThemes[19] = 40;   backgroundThemes[19] = 40;   wallThemes[19] = 40;     shapesThemes[19] = 40;    discountUnlockScores[19] = 25;
        musicThemes[20] = 40;   backgroundThemes[20] = 40;   wallThemes[20] = 40;     shapesThemes[20] = 40;    discountUnlockScores[20] = 25;
        musicThemes[21] = 40;   backgroundThemes[21] = 40;   wallThemes[21] = 40;     shapesThemes[21] = 40;    discountUnlockScores[21] = 25;
        musicThemes[22] = 40;   backgroundThemes[22] = 60;   wallThemes[22] = 80;     shapesThemes[22] = 60;    discountUnlockScores[22] = 125;
        musicThemes[23] = 40;   backgroundThemes[23] = 40;   wallThemes[23] = 40;     shapesThemes[23] = 40;    discountUnlockScores[23] = 25;
        musicThemes[24] = 40;   backgroundThemes[24] = 40;   wallThemes[24] = 40;     shapesThemes[24] = 40;    discountUnlockScores[24] = 25;
        musicThemes[25] = 40;   backgroundThemes[25] = 40;   wallThemes[25] = 40;     shapesThemes[25] = 40;    discountUnlockScores[25] = 25;
        musicThemes[26] = 40;   backgroundThemes[26] = 40;   wallThemes[26] = 40;     shapesThemes[26] = 40;    discountUnlockScores[26] = 25;
        musicThemes[27] = 40;   backgroundThemes[27] = 40;   wallThemes[27] = 40;     shapesThemes[27] = 40;    discountUnlockScores[27] = 25;
        musicThemes[28] = 40;   backgroundThemes[28] = 40;   wallThemes[28] = 40;     shapesThemes[28] = 40;    discountUnlockScores[28] = 25;
    }

}
