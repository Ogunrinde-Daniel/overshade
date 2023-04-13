using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    public enum GameState { IDLE, RUNNING, JUMPING, ATTACKING, SHOOTING };  //this order must be in sync with the animator
    public GameState currentState;       //stores the current state of the user
    //movement
    public float dirX;                   //the direction the parent is facing
    public int flipX = 1;                //reset to -1 for parent objects facing left by default
    public bool jumpable;                //checks if the parent is on the ground
    //mechanics                          
    public bool isDead;                  //is the parent object dead
    public float hitPoint;               //the amt of damage the parent gives out when he attacks
    //health                             
    public float health;                 //the current health of the parent
    public float maxHealth;              //the maxHealth the parent can attain 
    //user properties
    private Rigidbody2D rb;
    private Animator animator;
    //attack info
    public float attackCoolDown = 0;    //static->time before the player can attack again
    private float lastAttackTime = 0;   //calculates the time since the last attack
    //stat info
    public float speed;                 //movement speed
    public float jumpForce;
    public float attackRange;           //how big the attack range(radius around the attack point)
    public float attackAnimDelay = 0;   //how long it takes before the attack animation finishes
    //object parameters
    public GameObject slider;           
    public Transform attackPoint;
    public LayerMask enemiesLayerMask;
    public GameManager gameManager;
    public Soundmanager soundManager;
    //prefabs
    public GameObject bullets;
    public Transform bulletSpawn;
    public bool activateSound = false;

    void Start()
    {
        //initilize parameters
        isDead = false;
        jumpable = true;
        currentState = GameState.IDLE;

        lastAttackTime = attackCoolDown;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<Soundmanager>();
    }
    void Update()
    {
        if (isDead) return;
        //if the player is not moving or jumping-- set the animation to idle
        if ((dirX > 0.1 || dirX < 0.1) && jumpable) currentState = GameState.IDLE;
        //reduce the lastAttackTime
        if(lastAttackTime > 0) lastAttackTime -= Time.deltaTime;
        //reset player orientation
        resetFlip();

    }
    public void resetFlip()
    {
        //flip right
        if (dirX > 0)
            transform.localScale = new Vector3(1 * flipX, 1, 1);
        //flip left
        else if (dirX < 0)
            transform.localScale = new Vector3(-1 * flipX, 1, 1);
    }

    //uncompulsoryFlip is for objects which face left at the start of the game(-1)
    public void move(int uncompulsoryFlip = 1)
    {
        //move forward
        if(rb.velocity.y > 0) //if we are jumping -- use half the move speed
        {
            rb.velocity = new Vector2(dirX * (speed / 2), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        }
        currentState = GameState.RUNNING;
        //if we are currently not on the ground, set the state to jumping
        if (!jumpable) currentState = GameState.JUMPING;
    }
    public void jump()
    {
        if(jumpable) rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        currentState = GameState.JUMPING;
        //-play sound
        soundManager.jump.Play();

    }
    public void doubleJump()
    {
        //set logic for double jump: jump function can be called here

    }
    public void takeDamage(float damagePoint)
    {
        health -= damagePoint;
        slider.GetComponent<Slider>().value = health / maxHealth;
        if(activateSound)soundManager.swordHit.Play();

        //-add a shield strength logic here

        if (health > 0)
        {
            //trigger hurt animation
            animator.SetTrigger("Hurt");
        }
        else
        {
            gameManager.shakeCamera();
            //trigger death animation
            animator.SetTrigger("Death");
            if (GetComponent<BoxCollider2D>() != null)
                GetComponent<BoxCollider2D>().isTrigger = true;
            if (GetComponent<Rigidbody2D>() != null)
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            isDead = true;
        }
    }
    public void attack()
    {
        if (lastAttackTime > 0) return;
        if(activateSound)soundManager.swordSwosh.Play();
        currentState = GameState.ATTACKING;
        animator.SetTrigger("Attack");
        //apply damage to your enemies after a set time
        Invoke(nameof(attackContinous), attackAnimDelay);
        
        //reduce the combatBar value
        if (GetComponent<CombatBar>() != null)
            GetComponent<CombatBar>().reduceValue();
        //reset the attackTimer
        lastAttackTime = attackCoolDown;
    }
    private void attackContinous()
    {
        //detect enemies
        Collider2D[] enemiesTouched = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemiesLayerMask);
        //attack enemies
        foreach (var enemy in enemiesTouched)
        {
            enemy.gameObject.GetComponent<Combat>().takeDamage(hitPoint);
        }
    }

    public void shoot(float xDir,float damage, float speed)
    {
        Instantiate(bullets);
        bullets.GetComponent<shoot>().Initialize(xDir, speed, damage, bulletSpawn, this.gameObject);
    }
    public void updateAnimation()
    {
        animator.SetInteger("AnimState", (int)currentState);
        /*
        switch (currentState) {
            case GameState.RUNNING:
                animator.SetInteger("AnimState", (int)currentState);
            break;
        }
        */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jumpable"))
        {
            jumpable = true;
        }
        if (collision.CompareTag("Bullets"))
        {
            if(collision.gameObject.GetComponent<shoot>().parent != this.gameObject)
                takeDamage(collision.gameObject.GetComponent<shoot>().damage);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Jumpable"))
        {
            jumpable = false;
        }
    }
    private void OnDrawGizmosSelected()
    {    //editor scripting
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
