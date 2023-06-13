using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour, IDataPersistence
{
    public static MoneyManager instance;

    private int scrap;
    public int Scrap{ get { return scrap; } set { scrap = value; }}
    private int money;
    public int Money{ get { return scrap; } set { scrap = value; }}

    private void Awake()
    {
        instance = this;
    }

    public void LoadData(GameData data)
    {
        scrap = data.scrap;
        money = data.money;
    }

    public void SaveData(ref GameData data)
    {
        data.money = money;
        data.scrap = scrap;
    }

    public void AddScrapAmount(int _scrap)
    {
        scrap += _scrap;
    }
    public void AddMoneyAmount(int _money)
    {
        money += _money;
    }
}
