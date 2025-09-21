using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoost2 : MonoBehaviour
{
    //public float boost = 10f;
    //public float timer = 0f;

    public bool hasCollided = false;
    //[SerializeField] private float addAmount;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hasCollided)
        {
            var playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();

            Destroy(gameObject);
            hasCollided = true;
            playerAttack.DamagePotion();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
