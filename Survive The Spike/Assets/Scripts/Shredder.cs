using UnityEngine;

public class Shredder : MonoBehaviour
{
    // These shredders destroy projectiles when the 2 collide
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Projectile")
            Destroy(col.gameObject);
    }
}
