using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsView : MonoBehaviour
{
   public TMP_Text totalMoneyText;

    void Start()
    {
        if (MoneySystem.instance != null)
        {
            UpdateMoneyText(MoneySystem.instance.CheckingMoney());
        }
    }

   private void OnEnable()
   {
        if (MoneySystem.instance != null) {
            MoneySystem.OnMoneyAdded += UpdateMoneyText;
            MoneySystem.OnMoneySpent += UpdateMoneyText;

        }
   }
   
   private void OnDisable()
   {
if (MoneySystem.instance != null) {
            MoneySystem.OnMoneyAdded -= UpdateMoneyText;
            MoneySystem.OnMoneySpent -= UpdateMoneyText;

        }
   }

   private void UpdateMoneyText(float money)
   {
    totalMoneyText.text = "Total Money: $" + money;
   }
}
