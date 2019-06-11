using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] Transform player;
    //Items:
    int coinCount;
    //Item #1
    [Header("Bomb")]
    [SerializeField] GameObject bombPrefab;
    [SerializeField] TextMeshProUGUI bombCountText;
    int bombCount = 0;
    int bombPrice;

    //Item #2
    [Header("Bow")]
    [SerializeField] GameObject bowPrefab;
    [SerializeField] TextMeshProUGUI bowCountText;
    int bowCount = 0;
    int bowPrice = 20;
    int bowPriceIncrease;
    int bowLevel;
    float bowRateOfFire;
    float bowDamage;

    private void Update() {
        coinCount = FindObjectOfType<gameMaster>().getCoins();
        inputListener();
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
