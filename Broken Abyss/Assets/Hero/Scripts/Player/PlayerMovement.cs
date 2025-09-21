using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float speedBoost = 2.5f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    private float attackTime = 0.3f;
    private float attackCounter = 0.3f;
    private bool isAttacking; // Check if attacking or not

    public Transform Aim;
    bool isWalking = false;

    private Inventory playerInventory;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameObject playerInventoryObject = GameObject.Find("HeroCanvas/PlayerInventory");
        playerInventory = playerInventoryObject.GetComponent<Inventory>();
    }

    public void SpeedPotion()
    {
        moveSpeed += speedBoost;
        StartCoroutine(SpeedBoostTimer());
    }

    private IEnumerator SpeedBoostTimer()
    {
        yield return new WaitForSeconds(10f);
        moveSpeed -= speedBoost;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        rb.velocity = movement * moveSpeed;
        animator.SetFloat(horizontal, movement.x);
        animator.SetFloat(vertical, movement.y);

        if(movement != Vector2.zero)
        {
            animator.SetFloat(lastHorizontal, movement.x);
            animator.SetFloat(lastVertical, movement.y);
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if(isWalking)
        {
            Vector3 vector3 = Vector3.left * InputManager.Movement.x + Vector3.down * InputManager.Movement.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }

        if(isAttacking)
        {
            attackCounter -= Time.deltaTime;
            if(attackCounter <= 0)
            {
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
        }

        if((Input.GetMouseButtonDown(0) && playerInventory.activeSlotIndexNum == 0)
            || (Input.GetMouseButtonDown(0) && playerInventory.isSword)
            || (Input.GetMouseButtonDown(0) && playerInventory.isMagicSword)
            || (Input.GetMouseButtonDown(0) && playerInventory.isLaserSword)
            || (Input.GetMouseButtonDown(0) && playerInventory.isIceSword)
            || (Input.GetMouseButtonDown(0) && playerInventory.isFlameSword)
            || (Input.GetMouseButtonDown(1) && playerInventory.activeSlotIndexNum == 0)
            || (Input.GetMouseButtonDown(1) && playerInventory.isSword)
            || (Input.GetMouseButtonDown(1) && playerInventory.isMagicSword)
            || (Input.GetMouseButtonDown(1) && playerInventory.isLaserSword)
            || (Input.GetMouseButtonDown(1) && playerInventory.isIceSword)
            || (Input.GetMouseButtonDown(1) && playerInventory.isFlameSword))
        {
            attackCounter = attackTime;
            animator.SetBool("isAttacking", true);
            isAttacking = true;
        }
    }
}
