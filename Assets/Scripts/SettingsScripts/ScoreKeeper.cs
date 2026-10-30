using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper 
{
	private static int bestCoins;
	private static int totalCoins;
    private static int bestScore;

	private static UserData userData;

	public ScoreKeeper()
	{
		bestScore = 0;
        bestCoins = 0;
		totalCoins = 0;
		LoadUserData(0);
	}

	//Saving Functions
	private static void LoadUserData(int slot = 0)
	{
		userData = new UserData();
		userData.totalCoins = PlayerPrefs.GetInt("totalCoins" + slot);

		userData.bestScore = PlayerPrefs.GetInt("bestScore" + slot);
		userData.bestCoins = PlayerPrefs.GetInt("bestCoins" + slot);

		LoadDataFromPacket();
	}

	private static void LoadDataFromPacket()
	{
		totalCoins = userData.totalCoins;

		bestScore = userData.bestScore;
		bestCoins = userData.bestCoins;
	}

	private static void SaveUserData(int slot = 0)
	{
		LoadDataToPacket();
		PlayerPrefs.SetInt("totalCoins" + slot, userData.totalCoins);

		PlayerPrefs.SetInt("bestScore" + slot, userData.bestScore);
		PlayerPrefs.SetInt("bestCoins" + slot, userData.bestCoins);

		PlayerPrefs.Save();
	}

	private static void LoadDataToPacket()
	{
		userData = new UserData();
		userData.totalCoins = totalCoins;

		userData.bestScore = bestScore;
		userData.bestCoins = bestCoins;
	}

	private static void SaveUserDataCoinsOnly(int slot = 0)
	{
		LoadDataToPacket();
		PlayerPrefs.SetInt("totalCoins" + slot, userData.totalCoins);
		PlayerPrefs.Save();
	}

	//coin functions
	public void AddCoins(int currentCoins)
	{
		totalCoins += currentCoins;
		SaveUserDataCoinsOnly(0);
	}

	public void SpendCoins(int amount)
	{
		totalCoins =  totalCoins - amount;
		SaveUserDataCoinsOnly(0);
	}


	// compare functions
	public void CompareAgainstBestScore(int currentScore)
	{
		if(currentScore > bestScore){
			bestScore = currentScore;
			SaveUserData(0);
			//Debug.Log("New Best Score: " + bestScore + " !!!!");
		}
		else {
			SaveUserDataCoinsOnly(0);
			//Debug.Log("Subpar Performance, Do Better.");
		}
	}

	public void CompareAgainstBestCoins(int currentCoins)
	{
		if(currentCoins > bestCoins){
			bestCoins = currentCoins;
            SaveUserData(0);
			//Debug.Log("New Best Coins: " + bestCoins + " !!!!");
		}
		else {
			//Debug.Log("Subpar Performance, Do Better.");
		}
		
	}


	// getting/setting functions
	//score
	public int GetBestScore()
	{
		return bestScore;
	}

	//coins
	public int GetBestCoins()
	{
		return bestCoins;
	}

	public int GetTotalCoins()
	{
		return totalCoins;
	}
}
