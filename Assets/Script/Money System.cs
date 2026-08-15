using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{   

    public static MoneySystem instance;    
    private float money;

    public static event Action<float> OnMoneyAdded;
    public static event Action<float> OnMoneySpent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
        //PlayerPrefs.SetFloat("Money", )
    }

    public void AddMoney(float money)
    {
        if (money <= 0)
        {
            Debug.LogWarning("Invalid money amount");
            return;
        }

        this.money += money;
        OnMoneyAdded?.Invoke(this.money);
    }

    public bool BuyingSomething(float cost)
    {
        if (!CanAfford(cost)) return false;

        money -= cost;
        OnMoneySpent?.Invoke(this.money);
        return true;
    }

    public bool CanAfford(float cost) => money >= cost;
    
    public float CheckingMoney() => money;
    // check if can afford
    // buy something 
    // check total money
}
