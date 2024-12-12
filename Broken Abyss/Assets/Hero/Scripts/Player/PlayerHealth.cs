using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float maxHealth = 100f;

    public bool revive;

    public bool isInvincible = false;

    public float RemainingHealthPercent
    {
        get
        {
            return currentHealth/maxHealth;
        }
    }

    public UnityEvent OnHealthChanged;

    public void InvinciblePotion()
    {
        isInvincible = true;
        StartCoroutine(InvincibleTimer());
    }

    private IEnumerator InvincibleTimer()
    {
        yield return new WaitForSeconds(15f);
        isInvincible = false;
    }

    public void RevivePotion(){
        revive = true;
        Debug.Log("Revive Active PlayerHealth");
    }

    public void TakeDamage(float damageAmount)
    {
        if(!isInvincible)
        {
            currentHealth -= damageAmount;
            
            OnHealthChanged.Invoke();

            if(currentHealth <= 0)
            {
                if (revive)
                {
                    currentHealth = maxHealth;
                    revive = false;
                }
                else
                {
                    currentHealth = 0;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                
            }
        }
    }

    public void AddHealth(float addAmount)
    {
        if(currentHealth == maxHealth)
        {
            return;
        }

        currentHealth += addAmount;

        OnHealthChanged.Invoke();

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        revive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
