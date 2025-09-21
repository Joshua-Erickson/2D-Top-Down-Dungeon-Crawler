using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    public void UseItem()
    {
        var inventory = GameObject.Find("HeroCanvas/PlayerInventory").GetComponent<Inventory>();
        var playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        var lightDamage = GameObject.Find("Player/Aim/Melee").GetComponent<lightAttack>();
        var heavyDamage = GameObject.Find("Player/Aim/Melee2").GetComponent<heavyAttack>();
        var playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        var playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();

        // Fruit Food Items
        if (inventory.isApple)
        {
            playerHealth.AddHealth(10);
        }
        if (inventory.isPineapple)
        {
            playerHealth.AddHealth(100);
        }
        if (inventory.isWatermelon)
        {
            playerHealth.AddHealth(50);
        }
        if (inventory.isStrawberry)
        {
            playerHealth.AddHealth(25);
        }

        // Weapons
        if (inventory.isDagger)
        {
            lightDamage.damage = -10f;
            heavyDamage.damage = -15f;
        }
        if (inventory.isSword)
        {
            lightDamage.damage = -15f;
            heavyDamage.damage = -20f;
        }
        if (inventory.isMagicSword)
        {
            lightDamage.damage = -20f;
            heavyDamage.damage = -25f;
        }
        if(inventory.isIceSword)
        {
            lightDamage.damage = -25f;
            heavyDamage.damage = -30f;
        }
        if(inventory.isFlameSword)
        {
            lightDamage.damage = -30f;
            heavyDamage.damage = -35f;
        }
        if(inventory.isLaserSword)
        {
            lightDamage.damage = -35f;
            heavyDamage.damage = -40f;
        }

        // Potions
        if (inventory.isSpeedPotion)
        {
            playerMovement.SpeedPotion();
        }
        if (inventory.isDamagePotion)
        {
            playerAttack.DamagePotion();
        }
        if (inventory.isInvinciblePotion)
        {
            playerHealth.InvinciblePotion();
        }
        if (inventory.isRevivePotion)
        {
            Debug.Log("Revive Active ItemsAtt");
            playerHealth.RevivePotion();
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
