using Unity.VisualScripting;
using UnityEngine;

public class U_Enemy : MonoBehaviour
{
    public enum AttackStates{ REST, SWORD_SWING, SHOOTING, GROUND_PUNCH, NONE};
    public float[] weight = new float[] { 0.1f, 0.6f, 0.2f, 0.1f };//sum must be equal to 1, size must be AttackStates size - 1(none)
    private float[] duration = new float[] {5,10,5,2 };
    private float currentAttackDuration = 0f;
    private bool groundPunched = false;
    private AttackStates currentAttackState = AttackStates.REST;

    private Combat combat;
    [SerializeField] private float hitPoint;
    [SerializeField] private float maxHealth;
    [SerializeField] private float groundPunchHitPoint;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float attackRange;

    [SerializeField] private GameObject slider;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemiesLayerMask;
    
    [SerializeField]private Transform whoToAttack;

    [SerializeField] private float shootTimer;
    [SerializeField] private float SHOOT_DELAY;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private GameObject bullets;
    [SerializeField] private Transform bulletSpawn;

    void Start()
    {
        combat = gameObject.AddComponent<Combat>();

        combat.health = maxHealth;
        combat.maxHealth = maxHealth;

        combat.speed = speed;
        combat.jumpForce = jumpForce;

        combat.hitPoint = hitPoint;
        combat.attackRange = attackRange;
        combat.attackPoint = attackPoint;

        combat.slider = slider;
        combat.enemiesLayerMask = enemiesLayerMask;
        combat.jumpable = true;

        combat.attackCoolDown = 2.5f;
        combat.flipX = -1;

        combat.bullets = bullets;
        combat.bulletSpawn = bulletSpawn;

    }

    private void Update()
    {
        if (combat.isDead || combat.health <= 0)
        {
            if(!combat.gameManager.winScreenActive) combat.gameManager.setWinScreen("Player Wins");
            return;
        }
        if (whoToAttack.gameObject.GetComponent<Combat>().isDead)
        {
            if(!combat.gameManager.winScreenActive) combat.gameManager.setWinScreen("Player Lost");
            win();
            return;
        }
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

        currentAttackDuration += Time.deltaTime;

        attackAi();
        combat.updateAnimation();
    }

    private void attackAi()
    {
        //face player
        combat.dirX = whoToAttack.position.x > GetComponent<Rigidbody2D>().position.x ? 1 : -1;

        if (currentAttackState == AttackStates.NONE)
        {
            currentAttackState = (AttackStates) generateRandomNumber(weight);
            Debug.Log(currentAttackState.ToString());
        }


        switch (currentAttackState)
        {
            case AttackStates.REST:
                rest();
                break;
            case AttackStates.SWORD_SWING:
                swingSword();
                break;
            case AttackStates.SHOOTING:
                shoot();
                break;
            case AttackStates.GROUND_PUNCH:
                groundPunch();
                break;
        }

    }
    private void groundPunch()
    {
        //punch once and return; - time must be 0
        if (!groundPunched)
        {
            groundPunched = true;   
            combat.gameManager.shakeCamera();
            whoToAttack.gameObject.GetComponent<Combat>().takeDamage(groundPunchHitPoint);
        }
        if (currentAttackDuration >= duration[(int)currentAttackState])
        {
            groundPunched = false;
            currentAttackDuration = 0;
            currentAttackState = AttackStates.NONE;
            return;
        }
    }

    private void rest()
    {
        if (currentAttackDuration >= duration[(int)currentAttackState])
        {
            currentAttackDuration = 0;
            currentAttackState = AttackStates.NONE;
            return;
        }
    }


    private void swingSword()
    {
        if (currentAttackDuration >= duration[(int)currentAttackState])
        {
            currentAttackDuration = 0;
            currentAttackState = AttackStates.NONE;
            return;
        }
        if (inAttackRange())
        {
            combat.dirX = 0;    //stop movement
            combat.Invoke("attack", 2.5f);
        }

        combat.move(-1);

    }

    private void shoot()
    {
        combat.dirX = 0;    //stop movement

        if (currentAttackDuration >= duration[(int)currentAttackState])
        {
            currentAttackDuration = 0;
            currentAttackState = AttackStates.NONE;
            return;
        }
        if (shootTimer <= 0)
        {
            combat.shoot(-transform.localScale.x, bulletDamage, bulletSpeed);
            shootTimer = SHOOT_DELAY;
        }
    }



    private bool inAttackRange()
    {
        //detect player(s)
        Collider2D[] playersTouched = Physics2D.OverlapCircleAll(combat.attackPoint.position, combat.attackRange, combat.enemiesLayerMask);
        //can attack player(s) ?
        return (playersTouched.Length > 0);
             
    }


    private void win()
    {
        combat.currentState = Combat.GameState.IDLE;
        combat.updateAnimation();

    }

    public int generateRandomNumber(float[] weights)
    {
        float totalWeight = 0f;
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        float randomValue = UnityEngine.Random.value * totalWeight;
        for (int i = 0; i < weights.Length; i++)
        {
            if (randomValue < weights[i])
            {
                return i;
            }
            randomValue -= weights[i];
        }

        return weights.Length - 1;
    }
}
