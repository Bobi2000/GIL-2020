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

    private BigSpawner spawner = new BigSpawner();

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
        
        
        var Vector2Position = new Vector2(moveTo.x, moveTo.y);
        Vector3 lookpos = Camera.main.ScreenToViewportPoint(Vector2Position);
        float angle = Mathf.Atan2(moveTo.y, moveTo.x) * Mathf.Rad2Deg;
        this.gameObject.GetComponent<SpriteRenderer>().transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
    private void Attack()
    {
        if (CurrentTarget != null)
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
            spawner.KillEnemy();
        }
    }

    Vector2 offset(Vector2 end, Vector2 start)
    {
        float offsetX = 1f;
        float offsetY = 1f;

        float x = 0;
        float y = 0;
        if (end.x > start.x)
        {
            if (end.x > 0)
            {
                x = end.x - offsetX;
            }
            else if (end.x < 0)
            {
                x = end.x + offsetX;
            }
        }else if (end.x < start.x)
        {
            if (end.x > 0)
            {
                x = end.x + offsetX;
            }
            else if (end.x < 0)
            {
                x = end.x - offsetX;
            }
        }
        else if (end.x == 0)
        {
           x = end.x;
        }


        if (end.y > start.y)
        {
            if (end.y > 0)
            {
                y = end.y - offsetY;
            }
            else if (end.y < 0)
            {
                y = end.y + offsetY;
            }
        }
        else if (end.y < start.y)
        {
            if (end.y > 0)
            {
                y = end.y + offsetY;
            }
            else if (end.y < 0)
            {
                y = end.y - offsetY;
            }
        }else if (end.y == 0)
        {
            y = end.y;
        }
        return new Vector2(x, y);

        

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (moveTo.x == 0 && moveTo.y == 0 && collision.tag == "Turret")
        {
            Vector2 temp = offset(collision.transform.position, transform.position);
            moveTo.x = temp.x;
            moveTo.y = temp.y;

            Vector3 lookingAt = new Vector3(collision.transform.localPosition.x, collision.transform.localPosition.y, 0);
            transform.LookAt(lookingAt);
            
            this.CurrentTarget = collision.GetComponent<TowerController>();
            
        }
     

    }
}
