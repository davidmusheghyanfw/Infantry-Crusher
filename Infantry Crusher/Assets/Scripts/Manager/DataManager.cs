using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private void Awake()
    {
        instance = this;
    }
    public int GetLevelNumber()
    {
        int currentLevelNumber = 1;

        if (!PlayerPrefs.HasKey("levelNumber"))
        {
            PlayerPrefs.SetInt("levelNumber", currentLevelNumber);
        }

        currentLevelNumber = PlayerPrefs.GetInt("levelNumber", currentLevelNumber);

        return currentLevelNumber;

    }

    public void IncreaseLevelNumber()
    {
        PlayerPrefs.SetInt("levelNumber", GetLevelNumber() + 1);
    }

    public void SetCoinsAmount(int newTotalCoins)
    {
        PlayerPrefs.SetInt("coinsAmount", GetCoinsAmount() + newTotalCoins);
    }
    public int GetCoinsAmount()
    {
        int coinsAmount = 0;
        if (!PlayerPrefs.HasKey("coinsAmount"))
        {
            PlayerPrefs.SetInt("coinsAmount", coinsAmount);
        }

        coinsAmount = PlayerPrefs.GetInt("coinsAmount", coinsAmount);

        return coinsAmount;
    }


    public void ClearStats()
    {
        PlayerPrefs.DeleteAll();
    }

}
