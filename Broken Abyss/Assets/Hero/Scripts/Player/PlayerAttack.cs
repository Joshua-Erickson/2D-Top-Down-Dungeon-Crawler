using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Melee;
    public GameObject Melee2;
    private bool isAttacking = false; // Check for light attack
    private bool isAttacking2 = false; // Check for heavy attack
    private float attackDuration = 0.1f; // Light attack
    private float attackDuration2 = 0.3f; // Heavy attack
    private float attackTimer = 0f;

    private Inventory playerInventory;

    public float damageBoost = 20f;

    public lightAttack lightDamage;
    public heavyAttack heavyDamage;

    void Awake()
    {
        GameObject playerInventoryObject = GameObject.Find("HeroCanvas/PlayerInventory");
        playerInventory = playerInventoryObject.GetComponent<Inventory>();

        lightDamage = GameObject.Find("Player/Aim/Melee").GetComponent<lightAttack>();
        heavyDamage = GameObject.Find("Player/Aim/Melee2").GetComponent<heavyAttack>();
    }

    public void DamagePotion()
    {
        lightDamage.damage -= damageBoost;
        heavyDamage.damage -= damageBoost;
        StartCoroutine(DamageBoostTimer());
    }

    private IEnumerator DamageBoostTimer()
    {
        yield return new WaitForSeconds(5f);
        lightDamage.damage += damageBoost;
        heavyDamage.damage += damageBoost;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();


        if((Input.GetMouseButtonDown(0) && playerInventory.activeSlotIndexNum == 0) 
            || (Input.GetMouseButtonDown(0) && playerInventory.isSword)
            || (Input.GetMouseButtonDown(0) && playerInventory.isMagicSword)
            || (Input.GetMouseButtonDown(0) && playerInventory.isLaserSword)
            || (Input.GetMouseButtonDown(0) && playerInventory.isIceSword)
            || (Input.GetMouseButtonDown(0) && playerInventory.isFlameSword))
        {
            OnAttack();
        }
        if((Input.GetMouseButtonDown(1) && playerInventory.activeSlotIndexNum == 0)
            || (Input.GetMouseButtonDown(1) && playerInventory.isSword)
            || (Input.GetMouseButtonDown(1) && playerInventory.isMagicSword)
            || (Input.GetMouseButtonDown(1) && playerInventory.isLaserSword)
            || (Input.GetMouseButtonDown(1) && playerInventory.isIceSword)
            || (Input.GetMouseButtonDown(1) && playerInventory.isFlameSword))
        {
            OnAttack2();
        }
    }

    void OnAttack() // Light Attack
    {
        if(!isAttacking)
        {
            Melee.SetActive(true);
            isAttacking = true;
        }
    }

    void OnAttack2() // Heavy Attack
    {
        if(!isAttacking2)
        {
            Melee2.SetActive(true);
            isAttacking2 = true;
        }
    }

    void CheckMeleeTimer() 
    {
        if(isAttacking)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackDuration)
            {
                attackTimer = 0;
                isAttacking = false;
                Melee.SetActive(false);
            }
        }

        if(isAttacking2)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackDuration2)
            {
                attackTimer = 0;
                isAttacking2 = false;
                Melee2.SetActive(false);                    
            }
        }
    }
}
