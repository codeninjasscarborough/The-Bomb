using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsView : MonoBehaviour
{
   public TMP_Text totalMoneyText;
   public TMP_Text damageText;
   public TMP_Text moneyMulttext;
   public TMP_Text riskText;
   public TMP_Text ratingText;
   



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
