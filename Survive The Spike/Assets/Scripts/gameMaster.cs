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



    private int coinCount = 0;
    bool isLeaching = false;
    //this bool is used to make sure that leaching is not overlapped (called again before it is finished)

    

    private void Awake() {
        playerScript = FindObjectOfType<Player>();
        playerMovementScript = FindObjectOfType<playerMovement>();
        timeToAddCoin = (float)addCoinInterval;
    }

    private void Start() {
        if(playerScript == null) 
            Debug.LogError("Can't find object of type: player");
        if(coinText == null) 
            Debug.LogError("Can't find reference to coin text UI");

        if (playerMovementScript == null) 
            Debug.LogError("Can't find object of type: playerMovement");
    }

    private void Update() {
        coinText.text = "Coins: " + coinCount.ToString();
        ConstantlyAddCoins();

    }

    private void ConstantlyAddCoins() {
        //coinCount = (int)(Mathf.Floor(Time.time) * .25f);
        
        if(Time.time > timeToAddCoin) {
            coinCount++;
            timeToAddCoin = addCoinInterval + Time.time;
        }

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
        Debug.Log(_projectile.gameObject.name + " In FadeDestroy animation");
        isLeaching = false;
    }
}
