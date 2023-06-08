using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public static MoneyController instance;

    private float scrap;
    public float Scrap{ get { return scrap; } set { scrap = value; }}
    private float money;
    public float Money{ get { return scrap; } set { scrap = value; }}

    private void Awake()
    {
        instance = this;
    }

}
