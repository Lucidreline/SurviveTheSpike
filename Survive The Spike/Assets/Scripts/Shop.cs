using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Transform player;
    int upgradeLevelCap = 10;
    [SerializeField] int upgradePriceIncrease = 5;
    [SerializeField] Image[] itemBackgrounds;
    [SerializeField] Image hi;

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
    [SerializeField] float increaseRateOfFireBy = .04f;

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
        //itemBackgrounds[0].color = new Color(255, 255, 0);
        //hi.color = new Color(0, 255, 0);
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
            StartCoroutine(colorResponce(true, 2));
            //Bomb
            FindObjectOfType<gameMaster>().addCoins(-bombPrice);
            bombCount++;
        } else {
            StartCoroutine(colorResponce(false, 2));
        }
        if(itemNum == 2 && coinCount >= bowPrice) {
            //Bow
            StartCoroutine(colorResponce(true, 0));
            FindObjectOfType<gameMaster>().addCoins(-bowPrice);
            bowCount++;
        } else {
            StartCoroutine(colorResponce(false, 0));
        }
    }

    public void heal() {
        if(healthCount < FindObjectOfType<Player>().maxHealth && coinCount >= healPrice) {
            StartCoroutine(colorResponce(true, 1));
            FindObjectOfType<gameMaster>().addCoins(-healPrice);
            FindObjectOfType<Player>().health += healAmmount;

            if (FindObjectOfType<Player>().health > FindObjectOfType<Player>().maxHealth) {
                FindObjectOfType<Player>().health = FindObjectOfType<Player>().maxHealth;
            }

        } else {
            StartCoroutine(colorResponce(false, 1));
        }
    }

    public void inreaseMaxHealth() {
        if(coinCount >= increaseMaxHealthPrice) {
            StartCoroutine(colorResponce(true, 5));
            healthLevel++;
            FindObjectOfType<gameMaster>().addCoins(-increaseMaxHealthPrice);
            FindObjectOfType<Player>().maxHealth += IncreaseHealthMaxBy;
            increaseMaxHealthPrice += upgradePriceIncrease;
        } else {
            StartCoroutine(colorResponce(false, 5));
        }
    }

    public void increaseMovementSpeed() {
        if (movementSpeedLevel < upgradeLevelCap && coinCount >= increaseMovementSpeedPrice) {
            StartCoroutine(colorResponce(true, 6));
            FindObjectOfType<gameMaster>().addCoins(-increaseMovementSpeedPrice);
            movementSpeedLevel++;
            FindObjectOfType<playerMovement>().playerMovementSpeed += increaseMovementSpeedBy;
            increaseMovementSpeedPrice += upgradePriceIncrease;
        } else {
            StartCoroutine(colorResponce(false, 6));
        }
    }

    public void increaseAttackDamage() {
        if(attackDamageLevel < upgradeLevelCap && coinCount >= increaseAttackDamagePrice) {
            StartCoroutine(colorResponce(true, 7));
            FindObjectOfType<gameMaster>().addCoins(-increaseAttackDamagePrice);
            attackDamageLevel++;
            FindObjectOfType<arm>().attackDamage += increaseAttackDamageby;
            increaseAttackDamagePrice += upgradePriceIncrease;
        } else {
            StartCoroutine(colorResponce(false, 7));
        }
    }

    public void increaseBowRateOfFire() {
        if (bowRateOfFireLevel < upgradeLevelCap && coinCount >= bowIncreaseRatePrice) {
            StartCoroutine(colorResponce(true, 8));
            FindObjectOfType<gameMaster>().addCoins(-bowIncreaseRatePrice);
            FindObjectOfType<Bow>().UpgradeFireRate(increaseRateOfFireBy);
            bowRateOfFireLevel++;
            bowIncreaseRatePrice += upgradePriceIncrease;
        } else {
            StartCoroutine(colorResponce(false, 8));
        }
    }

    public void increaseBowDamage() {
        if(bowDamageLevel < upgradeLevelCap && coinCount >= bowIncreaseDamagePrice) {
            StartCoroutine(colorResponce(true, 9));
            FindObjectOfType<gameMaster>().addCoins(-bowIncreaseDamagePrice);
            FindObjectOfType<Arrow>().upgradeDamage(bowIncreaseDamageBy);
            bowDamageLevel++;
            bowIncreaseDamagePrice += upgradePriceIncrease;
        } else {
            StartCoroutine(colorResponce(false, 9));
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

    IEnumerator  colorResponce(bool wentThrough, int backgroundNum) {
        //0 - Buy Bow
        //1 - Buy Heal
        //2 - Buy Bomb

        //3 - Player Upgrades
        //4 - Bow Upgrades

        //5 - Max health
        //6 - Movement Speed
        //7 - Damage

        //8 -  Rate of fire
        //9 - Bow Damage

        Color ogColor = itemBackgrounds[backgroundNum].color;
        Color newColor = new Color(255, 255, 255);

        if (wentThrough) {
            itemBackgrounds[backgroundNum].color = Color.green;
        } else {
            itemBackgrounds[backgroundNum].color = Color.red;
        }

        yield return new WaitForSeconds(.1f);
        itemBackgrounds[backgroundNum].color = Color.white;

    }

    public void outsideColorChanges(string whichBackground) {
        if (whichBackground == "Player Upgrades") {
            StartCoroutine(colorResponce(true, 3));
        } else {
            StartCoroutine(colorResponce(true, 4));
        }

    }
}
