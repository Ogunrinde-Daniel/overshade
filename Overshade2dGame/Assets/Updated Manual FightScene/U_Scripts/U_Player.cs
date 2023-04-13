using UnityEngine;

public class U_Player : MonoBehaviour
{
    private Combat combat;
    [SerializeField] private float hitPoint;
    [SerializeField] private float maxHealth;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCoolDown = 0f;


    [SerializeField] private GameObject slider;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemiesLayerMask;
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
        combat.attackCoolDown = attackCoolDown;

        combat.slider = slider;
        combat.enemiesLayerMask = enemiesLayerMask;

        combat.activateSound = true;
    }

    void Update()
    {
        if (combat.isDead) return;
        getInput();
        combat.updateAnimation();
    }

    void getInput()
    {
        combat.dirX = Input.GetAxis("Horizontal");
        if (combat.dirX > 0 || combat.dirX < 0) combat.move();

        bool canAttack = GetComponent<CombatBar>().canAttack();
        if (Input.GetKeyDown(KeyCode.B) && canAttack) combat.attack();

        if (Input.GetKeyDown(KeyCode.Space)) combat.jump();

    }

}
