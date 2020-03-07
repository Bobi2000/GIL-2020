using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameObject enemy;
    private bool EnemyToShoot;
    private bool isDestroied = false;
    private static Vector2 direction;


    void Start()
    {

    }

    void Update()
    {
        if (EnemyToShoot)
        {
            if (enemy != null)
            {

                this.transform.position = Vector2.MoveTowards(this.transform.position, this.enemy.transform.position, Time.deltaTime * 10);
                direction = this.enemy.transform.position;
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }

    public void ShootEnemy(GameObject enemy)
    {
        this.enemy = enemy;
        EnemyToShoot = true;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        var enemyCollider = collider.GetComponent<EnemyController>();
        if (collider.gameObject.tag == "Enemys" && isDestroied == false)
        {
            collider.GetComponent<EnemyController>().TakeDamage(25);
            Destroy(gameObject);
            isDestroied = true;
        }
    }
}
