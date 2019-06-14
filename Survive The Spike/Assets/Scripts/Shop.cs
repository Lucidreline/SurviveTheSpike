using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] Transform player;
    //Items:
    //Item #1
    [Header("Bomb")]
    [SerializeField] GameObject bombPrefab;
    [SerializeField] TextMeshProUGUI bombCountText;
    [SerializeField] TextMeshProUGUI bombCountText2;
    [SerializeField] TextMeshProUGUI bombPriceText;
    int bombCount = 0;
    [SerializeField ]int bombPrice = 1;

    //Item #2
    [Header("Bow")]
    [SerializeField] GameObject bowPrefab;
    [SerializeField] TextMeshProUGUI bowCountText;
    [SerializeField] TextMeshProUGUI bowCountText2;
    [SerializeField] TextMeshProUGUI bowPriceText;
    int bowCount = 0;
    int bowPrice = 20;
    int bowPriceIncrease;
    int bowLevel;
    float bowRateOfFire;
    float bowDamage;

    [Header("Health")]
    [SerializeField] TextMeshProUGUI healthCountText;
    int healthCount;
    int healAmmount = 5;
    [SerializeField] int healPrice = 10;
    [SerializeField] TextMeshProUGUI healPriceText;

    int IncreaseHealthMaxBy = 10;
    [SerializeField] int increaseMaxHealthPrice = 30;
    [SerializeField] TextMeshProUGUI increaseMaxHealthText;


    [Header("Coins")]
    [SerializeField] TextMeshProUGUI coinCountText;
    int coinCount;

    private void Update() {
        coinCount = FindObjectOfType<gameMaster>().getCoins();
        healthCount = FindObjectOfType<Player>().health;
        // inputListener();
        updateCountText();

    }

    void updateCountText() {
        if(bombCountText2 != null) {
            bombCountText.text = bombCount.ToString();
            bombCountText2.text = bombCount.ToString();

            bowCountText.text = bowCount.ToString();
            bowCountText2.text = bowCount.ToString();
            bowPriceText.text = bowPrice.ToString();

            bombPriceText.text = bombPrice.ToString();

            healthCountText.text = healthCount.ToString() + "/" + FindObjectOfType<Player>().maxHealth.ToString();
            healPriceText.text = healPrice.ToString();
            increaseMaxHealthText.text = increaseMaxHealthPrice.ToString();

            coinCountText.text = coinCount.ToString();
            

        }  
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
        if(coinCount >= increaseMaxHealthPrice) {
            FindObjectOfType<gameMaster>().addCoins(-increaseMaxHealthPrice);
            FindObjectOfType<Player>().maxHealth += IncreaseHealthMaxBy;
        }
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
