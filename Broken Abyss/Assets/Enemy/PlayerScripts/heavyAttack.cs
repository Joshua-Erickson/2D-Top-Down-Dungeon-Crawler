using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heavyAttack : MonoBehaviour
{
    GameObject player;
    public float damage;
    public float knockback = 1000f;
    private void Start()
    {
        // Initialize player game object
        player = GetComponent<GameObject>();
    }
    // On trigger from game object collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collision is enemy,
        // Call UpdateHealth() and startHurt() from collision game object
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().UpdateHealth(damage,knockback);

            collision.gameObject.GetComponent<EnemyHealth>().startHurt();
        }
    }
}
