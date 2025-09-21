using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject Player;
    public GameObject Orc;
    private float health = 0f;
    [SerializeField] private float maxHealth = 100f;
    private Animator myAnim;
    public float EnemyCoinValue = 5;
    public GameObject Coin;

    public bool alreadyDead = false;
    public AudioSource[] audioSources;
    private Rigidbody2D rb;

    // Start is called on instantiation
    // Initialize needed objects and parameters
    private void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myAnim.SetBool("dead", false);
        Player = GameObject.Find("Player");
    }

    // UpdateHealth() is called by the player melee collider
    // Requires the amount of damage to be dealth and amount of knockback force
    public void UpdateHealth(float mod, float knockback)
    {
        // Play Sound
        PlayRandomSound();
        //Modify the health by amount
        health += mod;
        // If health is greater than max
        // Set lower to max health
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        // If health 0 or lower
        else if (health <= 0f)
        {
            // Die and spawn coins
            myAnim.SetBool("dead",true);
            if (!alreadyDead){
                for (int i = 0; i < EnemyCoinValue; i++)
                {
                    Instantiate(Coin, transform.position, Quaternion.identity);
                }
                alreadyDead = true;
            }
        }
        else
        {
            //Will Fix knockback later
            rb.AddForce((Orc.transform.position - Player.transform.position) * knockback);
        }
    }
    // Called by player attack script
    public void startHurt()
    {
        myAnim.SetBool("hurt", true);
        

    }
    //Called by trigger on Orc animation
    public void endHurt()
    {
        myAnim.SetBool("hurt", false);
    }
    //Called by trigger on Orc animation
    public void endDead()
    {
        Destroy(gameObject);
    }
    
    void PlayRandomSound()
    {
        // Pick a random index between 0 and the length of audioSources
        int randomIndex = Random.Range(0, audioSources.Length);

        // Check if the selected audio source is not already playing
        if (!audioSources[randomIndex].isPlaying)
        {
            audioSources[randomIndex].Play();
        }
    }
}
