using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceToTarget;
    [SerializeField] private int damage;
    [SerializeField] private float attacksPerSec;
    [SerializeField] private int moneyDropAmount;
    private float attackSpeed;
    private float timeSinceLastAttack;

    private void Start()
    {
        target = Player.Instance.transform;
        attackSpeed = 60 / attacksPerSec / 60;
    }

    private void FixedUpdate()
    {
        if (timeSinceLastAttack >= 0)
        {
            timeSinceLastAttack -= Time.deltaTime;
        }
        if (Vector3.Distance(transform.position, target.position) > distanceToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (timeSinceLastAttack < 0.0f)
        {
            {
                target.GetComponent<PlayerHealth>().TakeDamage(damage);
                timeSinceLastAttack = attackSpeed;
            }
        }
    }

    public int GetMoneyDropAmount()
    {
        return moneyDropAmount;
    }
}
