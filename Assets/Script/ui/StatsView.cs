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
   
    public BombDataBase dataBase;


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
        UpgradeButton.OnBombUpgraded += UpdateBombStats;
   }
   
   private void OnDisable()
   {
if (MoneySystem.instance != null) {
            MoneySystem.OnMoneyAdded -= UpdateMoneyText;
            MoneySystem.OnMoneySpent -= UpdateMoneyText;

        }
        UpgradeButton.OnBombUpgraded -= UpdateBombStats;
   }

   private void UpdateMoneyText(float money)
   {
    totalMoneyText.text = "Total Money: $" + money;
   }

   private void UpdateBombStats(int index)
    {
        damageText.text = "Damage: " + dataBase.GetBombAtIndex(index).damage;
        moneyMulttext.text = "Money Multipler: " + dataBase.GetBombAtIndex(index).moneyMult;
        riskText.text = $"Risk: {dataBase.GetBombAtIndex(index).risk} in 1";
        ratingText.text = $"Rating: {dataBase.GetBombAtIndex(index).rating} / 10";

    }
}
