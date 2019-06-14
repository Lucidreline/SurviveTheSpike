using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] Transform player;
    int upgradeLevelCap;
    [SerializeField] int upgradePriceIncrease = 25;

    //Items:
    //Item #1
    [Header("Bomb")]
    [SerializeField] GameObject bombPrefab;
    [SerializeField] TextMeshProUGUI[] bombCountText;
    [SerializeField] TextMeshProUGUI bombPriceText;
    int bombCount = 0;
    [SerializeField ]int bombPrice = 1;

    //Item #2
    [Header("Bow")]
    [SerializeField] GameObject bowPrefab;
    [SerializeField] TextMeshProUGUI[] bowCountText;
    [SerializeField] TextMeshProUGUI bowPriceText;
    int bowCount = 0;
    int bowPrice = 20;

    [Header("Bow Upgrades")]
    int bowRateOfFireLevel = 0, bowDamageLevel = 0;

    [Header("Rate of fire Upgrade")]
    [SerializeField] TextMeshProUGUI bowRateOfFireLevelText;
    [SerializeField] TextMeshProUGUI bowIncreaseRatePriceText;
    [SerializeField] int bowIncreaseRatePrice = 5;
    [SerializeField] int increaseRateOfFireBy = 5;

    [Header("Damage Upgrade")]
    [SerializeField] TextMeshProUGUI bowDamageLevelText;
    [SerializeField] TextMeshProUGUI bowIncreaseDamagePriceText;

    [SerializeField] int bowIncreaseDamagePrice = 5;
    [SerializeField] int bowIncreaseDamageBy = 5;


    [Header("Health")]
    [SerializeField] TextMeshProUGUI[] healthCountText;
    [SerializeField] TextMeshProUGUI healPriceText;
    [SerializeField] int healPrice = 10;
    int healthCount;
    int healAmmount = 5;

    [Header("Coins")]
    [SerializeField] TextMeshProUGUI[] coinCountText;
    int coinCount;

    [Header("Player Upgrades")]
    int healthLevel = 0, movementSpeedLevel = 0, attackDamageLevel = 0;

    [Header("Health Upgrade")]
    [SerializeField] TextMeshProUGUI increaseMaxHealthPriceText;
    [SerializeField] TextMeshProUGUI healthLevelText;
    [SerializeField] int increaseMaxHealthPrice = 30;
    int IncreaseHealthMaxBy = 10;

    [Header("Movement Speed Upgrade")]
    [SerializeField] TextMeshProUGUI increaseMovementSpeedPriceText;
    [SerializeField] TextMeshProUGUI MovementLevelText;
    [SerializeField] int increaseMovementSpeedPrice = 5;
    [SerializeField] int increaseMovementSpeedBy = 2;

    [Header("Attack Damage Upgrades")]
    [SerializeField] TextMeshProUGUI increaseAttackDamagePriceText;
    [SerializeField] TextMeshProUGUI attackdamageLevelText;
    [SerializeField] int increaseAttackDamagePrice = 5;
    [SerializeField] int increaseAttackDamageby = 10;

    

    private void Update() {
        coinCount = FindObjectOfType<gameMaster>().getCoins();
        healthCount = FindObjectOfType<Player>().health;
        // inputListener();
        updateCountText();
    }

    void updateCountText() {
        
        //Bomb Counter
        foreach(TextMeshProUGUI bombCounter in bombCountText) {
            bombCounter.text = bombCount.ToString();
        }

        //Bow Counter
        foreach(TextMeshProUGUI bowCounter in bowCountText) {
            bowCounter.text = bowCount.ToString();
        }
            
        //Health Counter
        foreach(TextMeshProUGUI healthCounter in healthCountText) {
            healthCounter.text = healthCount.ToString() + "/" + FindObjectOfType<Player>().maxHealth.ToString();
        }

        //Coin Counter
        foreach(TextMeshProUGUI coinCounter in coinCountText) {
            coinCounter.text = coinCount.ToString();
        } 

        //Purchase Prices
        healPriceText.text = healPrice.ToString();
        bowPriceText.text = bowPrice.ToString();
        bombPriceText.text = bombPrice.ToString();


        //Upgrade Prices
        increaseAttackDamagePriceText.text = increaseAttackDamagePrice.ToString();
        increaseMovementSpeedPriceText.text = increaseMovementSpeedPrice.ToString();
        increaseMaxHealthPriceText.text = increaseMaxHealthPrice.ToString();

        bowIncreaseDamagePriceText.text = bowIncreaseDamagePrice.ToString();
        bowIncreaseRatePriceText.text = bowIncreaseRatePrice.ToString();

        //Upgrade Levels
        attackdamageLevelText.text = attackDamageLevel.ToString();
        MovementLevelText.text = movementSpeedLevel.ToString();
        healthLevelText.text = healthLevel.ToString();

        bowDamageLevelText.text = bowDamageLevel.ToString();
        bowRateOfFireLevelText.text = bowRateOfFireLevel.ToString(); 
    }

    public int getBowCount() {
        return bowCount;
    }

    public void purchaseItem(int itemNum) {
        if(itemNum == 1 && coinCount >= bombPrice) {
            //Bomb
            FindObjectOfType<gameMaster>().addCoins(-bombPrice);
            bombCount++;
        }
        else if(itemNum == 2 && coinCount >= bowPrice) {
            //Bow
            FindObjectOfType<gameMaster>().addCoins(-bowPrice);
            bowCount++;
        }
    }

    public void heal() {
        if(healthCount < FindObjectOfType<Player>().maxHealth && coinCount >= healPrice) {
            FindObjectOfType<gameMaster>().addCoins(-healPrice);
            FindObjectOfType<Player>().health += healAmmount;

            if (FindObjectOfType<Player>().health > FindObjectOfType<Player>().maxHealth) {
                FindObjectOfType<Player>().health = FindObjectOfType<Player>().maxHealth;
            }
            
        }
    }

    public void inreaseMaxHealth() {
        if(coinCount >= increaseMaxHealthPrice && healthLevel < upgradeLevelCap) {
            healthLevel++;
            FindObjectOfType<gameMaster>().addCoins(-increaseMaxHealthPrice);
            FindObjectOfType<Player>().maxHealth += IncreaseHealthMaxBy;
            increaseMaxHealthPrice += upgradePriceIncrease;
        }
    }

    public void increaseMovementSpeed() {
        if (movementSpeedLevel < upgradeLevelCap && coinCount >= increaseMovementSpeedPrice) {
            FindObjectOfType<gameMaster>().addCoins(-increaseMovementSpeedPrice);
            movementSpeedLevel++;
            FindObjectOfType<playerMovement>().playerMovementSpeed += increaseMovementSpeedBy;
            increaseMovementSpeedPrice += upgradePriceIncrease;

        }
           
        
    }

    public void increaseAttackDamage() {

    }

    public void increaseBowRateOfFire() {

    }

    public void increaseBowDamage() {

    }

    //void inputListener() {
    //    if (Input.GetKeyDown(KeyCode.T)) {
    //        placeItem(2);
    //    }
    //}

    public void placeItem(int itemNum) {
        if(itemNum == 1 && bombCount > 0) {
            bombCount--;
            Instantiate(bombPrefab, player.position, player.rotation);
        }
        if(itemNum == 2 && bowCount > 0) {
            bowCount--;
            Instantiate(bowPrefab, player.position, player.rotation);
        }
    }

}
