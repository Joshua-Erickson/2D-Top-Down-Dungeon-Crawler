using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightAttack : MonoBehaviour
{
    public GameObject player;
    public float damage;
    public float knockback = 1000;

    private void Start()
    {
        // Initialize player game object
       // player = GetComponent<GameObject>();
    }
    // On trigger from collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // If collision is enemy,
            // Call UpdateHealth() and startHurt() from collision game object
            var Orchealth = collision.gameObject.GetComponent<EnemyHealth>();
            Orchealth.UpdateHealth(damage, knockback);
            Orchealth.startHurt();
            
        }
    }
}
