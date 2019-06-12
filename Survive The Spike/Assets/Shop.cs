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
    int bombPrice;

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

    [Header("Coins")]
    [SerializeField] TextMeshProUGUI coinCountText;
    int coinCount;

    private void Update() {
        coinCount = FindObjectOfType<gameMaster>().getCoins();
        healthCount = FindObjectOfType<Player>().getHealth();
        inputListener();
        updateCountText();

    }

    void updateCountText() {
        if(bombCountText2 != null) {
            //bombCountText.text = bombCount.ToString();
            bombCountText2.text = bombCount.ToString();

            bowCountText.text = bowCount.ToString();
            bowCountText2.text = bowCount.ToString();
            bowPriceText.text = bowPrice.ToString();

            healthCountText.text = healthCount.ToString();

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

    void inputListener() {
        if (Input.GetKeyDown(KeyCode.T)) {
            placeItem(2);
        }
    }

    void placeItem(int itemNum) {
        if(itemNum == 2 && bowCount > 0) {
            bowCount--;
            Instantiate(bowPrefab, player.position, player.rotation);
        }
    }

}
