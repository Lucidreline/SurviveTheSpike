using UnityEngine;

public class Shredder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Projectile")
            Destroy(col.gameObject);
    }
}
