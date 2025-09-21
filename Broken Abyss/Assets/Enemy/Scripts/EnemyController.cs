using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController : MonoBehaviour   
{
    public float speed = 1000f;
    public float attackDamage = 5f;
    public float attackSpeed = 1f;
    private float canAttack;
    public DetectionZone detectionzone;

    private Collider2D detecteObject0;
    public Animator myAnim;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        // Initialize the animation to be idle
        myAnim.SetBool("isAttacking", false);
        myAnim.SetBool("follow", false);
        canAttack = attackSpeed;
    }

    // FixedUpdate called every frame
    private void FixedUpdate()
    {
        // If there is a detected player object
        
        if (detectionzone.detectedObjs.Any())
        {
            // If the Orc is not dead or in the hurt animation
            // Create a force moving the Orc rigidbody towards the player object
            if ((myAnim.GetBool("dead") == false) || (myAnim.GetBool("hurt") == false))
            {

                Collider2D detectedObject0 = detectionzone.detectedObjs[0];
                Vector2 direction = (detectedObject0.transform.position - transform.position).normalized;

                rb.AddForce(direction * speed * Time.deltaTime);
                myAnim.SetBool("follow", true);
                myAnim.SetFloat("moveX", direction.x);
                myAnim.SetFloat("moveY", direction.y);

            }

        }
        // If the Orc is dead or hurt, stop the follow animation
        else
        {
            myAnim.SetBool("follow", false);
        }
    }

    // On collision with 2D collider:
    //      If collision is player object, and enough time has elapsed since last attack
    //      Enemy will do damage to player object
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                // Set animation to attack
                myAnim.SetBool("isAttacking", true);
                canAttack = 0f;
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                
            }
            else
            {
                // Increment attack time by time elapsed from last attack
                canAttack += Time.deltaTime;
            }
        }
    }
    // endAttack() called in attack animation stop the animation
    // from looping infinitly 
    public void endAttack()
    {
        // Stop attacking animation
        myAnim.SetBool("isAttacking",false);
    }
    
}
