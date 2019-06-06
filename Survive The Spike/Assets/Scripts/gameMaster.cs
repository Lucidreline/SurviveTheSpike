using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameMaster : MonoBehaviour
{
    Player playerScript;
    playerMovement playerMovementScript;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] int addCoinInterval = 6;
    float timeToAddCoin;

    [Header("Enemy Spawning")]
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform topSpawnMarker;
    [SerializeField] Transform rightSpawnMarker;
    [SerializeField] Transform bottomSpawnMarker;
    [SerializeField] Transform leftSpawnMarker;
    
    [SerializeField] int maxNumOfLiveEnemies = 0;
    [SerializeField] int liveEnemyCount = 0;

    [SerializeField] float maxEnemySpawnWidth = 5;
    [SerializeField] float maxEnemySpawnHeight = 5;

    [SerializeField] float EnemySpawnRate = 1.5f;
    float timeToSpawnEnemy = 0;

    [SerializeField] int raiseMaxLiveEnemiesInterval = 10;
    float timeToRaiseMaxEnemies = 0;

    // write method to constantly increase this like i did with the coins



    private int coinCount = 0;
    bool isLeaching = false;
    //this bool is used to make sure that leaching is not overlapped (called again before it is finished)

    

    private void Awake() {
        playerScript = FindObjectOfType<Player>();
        playerMovementScript = FindObjectOfType<playerMovement>();
        timeToAddCoin = (float)addCoinInterval;
    }

    private void Start() {
        //AKA void ErrorChecking() LOL

        if(playerScript == null) 
            Debug.LogError("Can't find object of type: player");
        if(coinText == null) 
            Debug.LogError("Can't find reference to coin text UI");

        if (playerMovementScript == null) 
            Debug.LogError("Can't find object of type: playerMovement");

        if(enemyPrefab == null)
            Debug.LogError("Can't find reference to Enemy Prefab");

        if (topSpawnMarker == null)
            Debug.LogError("Can't find reference to top marker");
        if (rightSpawnMarker == null)
            Debug.LogError("Can't find reference to right marker");
        if (bottomSpawnMarker == null)
            Debug.LogError("Can't find reference to bottom marker");
        if (leftSpawnMarker == null)
            Debug.LogError("Can't find reference to left marker");

    }

    private void Update() {
        coinText.text = "Coins: " + coinCount.ToString();
        ConstantlyAddCoins();
        if (Time.time > timeToSpawnEnemy) {
            EnemySpawning();
            timeToSpawnEnemy = EnemySpawnRate + Time.time;
        }
        
        ConstantlyRaiseMaxLiveEnemies();
    }

    private void ConstantlyRaiseMaxLiveEnemies() {
        if (Time.time > timeToRaiseMaxEnemies) {
            maxNumOfLiveEnemies++;
            timeToRaiseMaxEnemies = raiseMaxLiveEnemiesInterval + Time.time;
        }
    }

    private void ConstantlyAddCoins() {
        //coinCount = (int)(Mathf.Floor(Time.time) * .25f);
        
        if(Time.time > timeToAddCoin) {
            addCoins(1);
            timeToAddCoin = addCoinInterval + Time.time;
        }

    }

    public void addCoins(int ammount) {
        coinCount += ammount;
    }

    public void Leach(int durration, int damageASec, float moveMultiply, int moveAdd, bool ispermanent, Transform projectile = null){
        
        if (!isLeaching) {
            StartCoroutine(playerScript.DamageLeach(2, durration));
            StartCoroutine(playerMovementScript.MovementLeach(moveMultiply, moveAdd, durration, ispermanent, projectile));
            StartCoroutine(isLeachControl(durration, projectile));
        } else {
            Destroy(projectile.gameObject);
            Debug.Log(projectile.gameObject.name + " Destroyed because it hit player when player was already leaching");
            //make them have a small explosion or some effect
        } 
    }

    IEnumerator isLeachControl(int durration, Transform _projectile) {
        isLeaching = true;
        yield return new WaitForSeconds(durration);
        _projectile.gameObject.GetComponent<Animator>().SetBool("willDestroy", true);
        isLeaching = false;
    }

    public void AddToLiveEnemyCounter(int ammount) {
        liveEnemyCount += ammount;
    }

    void EnemySpawning() {
        //Step 1: Make sure the that the ammount of alive enemies does not exceed the maxNumOfAliveEnemies
        if(liveEnemyCount < maxNumOfLiveEnemies) {

            AddToLiveEnemyCounter(1);
            //Step 2: Choose which side the enemy will spawn. (Left, right, top, bottom)
            int whichSide = Random.Range(1, 5);
            Debug.Log(whichSide);
            //Step 3: Choose where on that side the enemie will spawn.
            float xPos = Random.Range(-maxEnemySpawnWidth, maxEnemySpawnWidth);
            float yPos = Random.Range(-maxEnemySpawnHeight, maxEnemySpawnHeight);
            Vector3 spawnPos = new Vector3(xPos, topSpawnMarker.position.y, topSpawnMarker.position.z);
                //added a default just incase;

            if (whichSide == 1) {
                //TOP
                spawnPos = new Vector3(xPos, topSpawnMarker.position.y, topSpawnMarker.position.z);
            }
            else if (whichSide == 2) {
                //RIGHT
                spawnPos = new Vector3(rightSpawnMarker.position.x, yPos, rightSpawnMarker.position.z);
            }
            else if (whichSide == 3) {
                //BOTTOM
                spawnPos = new Vector3(xPos, bottomSpawnMarker.position.y, bottomSpawnMarker.position.z);
            }
            else if (whichSide == 4) {
                //LEFT
                spawnPos = new Vector3(leftSpawnMarker.position.x, yPos, leftSpawnMarker.position.z);
            }


            //Step 4: Spawn Enemy
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            

        }

    }
}
