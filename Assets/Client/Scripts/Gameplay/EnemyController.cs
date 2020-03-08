using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float speed;
    public float attack;
    public float gold;

    private float timeBtwShots;
    public float startTimeBtwShots = 2f;

    public CircleCollider2D TurretMelleRange;

   // private BigSpawner spawner = new BigSpawner();

    private Vector2 moveTo = new Vector2(0, 0);
    private TowerController CurrentTarget;

    private bool canAttack = false;

    private void Start()
    {

    }

    private void Update()
    {
        Move();
        if (timeBtwShots <= 0 && CurrentTarget != null)
        {
            Attack();
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void Move()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, moveTo, Time.deltaTime * speed);
        if (this.CurrentTarget == null)
        {
            this.moveTo.x = 0;
            this.moveTo.y = 0;
        }

    }
    private void Attack()
    {
        if (canAttack && CurrentTarget != null)
        {
            CurrentTarget.DealTurretDamage(attack);
            var targetHp = CurrentTarget.CurrentHealth;
            if (targetHp <= 0 || CurrentTarget == null)
            {

                this.moveTo.x = 0;
                this.moveTo.y = 0;
                CurrentTarget = null;
                return;
            }
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (this.health <= 0)
        {
            Destroy(gameObject);
           // spawner.KillEnemy();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (moveTo.x == 0 && moveTo.y == 0 && collision.tag == "Turret")
        {
            moveTo.x = collision.transform.position.x;
            moveTo.y = collision.transform.position.y;
            this.CurrentTarget = collision.GetComponent<TowerController>();
            this.TurretMelleRange = collision.gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>();
        }
        if (this.gameObject != null && TurretMelleRange != null)
        {
            if (this.gameObject.GetComponent<CircleCollider2D>().bounds.Intersects(TurretMelleRange.bounds))
            {
                canAttack = true;
            }
        }

    }
}
