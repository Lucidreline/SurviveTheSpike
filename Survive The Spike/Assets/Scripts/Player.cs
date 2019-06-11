using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] int health = 100;
    int maxHealth;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] RectTransform healthBarTransform;

    private void Awake() {
        if (healthText == null)
            Debug.LogError("Can not find reference to health text!");
        maxHealth = health;
    }

    void Update(){
        healthText.text = "Health: " + health.ToString();
        updateHealthBar();
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

    void updateHealthBar() {
        float healthPercent = (float)health / maxHealth;
        healthBarTransform.localScale = new Vector3(healthPercent, healthBarTransform.localScale.y, healthBarTransform.localScale.z);
    }
}
