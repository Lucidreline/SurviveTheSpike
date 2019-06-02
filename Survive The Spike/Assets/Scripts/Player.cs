using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] TextMeshProUGUI healthText;

    void Update(){
        //Debug.Log("HEALTH: " + health);
        healthText.text = health.ToString();
    }

    public void DamagePlayer(int _damage) {
        health -= _damage;
    }
    public IEnumerator DamageLeach (int _damagePerSec, float _durration){
        for(int i = 0; i < _durration; i++){
        DamagePlayer(_damagePerSec);
        yield return new WaitForSeconds(1);
        } 
        //This for loop will loop every second.
    }
}
