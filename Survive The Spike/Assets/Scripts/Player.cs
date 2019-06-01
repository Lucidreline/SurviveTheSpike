using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int health = 100;

    public void DamagePlayer(int _damage){
        health -= _damage;
    }

    void Update(){
        Debug.Log("HEALTH: " + health);
    }

    public IEnumerator DamageLeach (int _damagePerSec, float _durration){
        for(int i = 0; i < _durration; i++){
        DamagePlayer(_damagePerSec);
        yield return new WaitForSeconds(1);
        } 
    }
}
