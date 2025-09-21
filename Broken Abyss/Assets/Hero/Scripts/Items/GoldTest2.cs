using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GoldTest2 : MonoBehaviour
{
    private bool hasCollided = false;
    [SerializeField] public int number = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var goldCounter = GameObject.Find("HeroCanvas/GoldCounter").GetComponent<GoldCounter>();

        if (collision.CompareTag("Player") && !hasCollided)
        {
            Destroy(gameObject);
            hasCollided = true;
            goldCounter.RemoveGold(number);
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
