using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameObject enemy;
    private bool EnemyToShoot;
    private bool isDestroied = false;
    private static Vector2 direction;

    private float DamageToDeal;

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

    public void ShootEnemy(GameObject enemy,float damage)
    {
        this.enemy = enemy;
        EnemyToShoot = true;
        DamageToDeal = damage;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        var enemyCollider = collider.GetComponent<EnemyController>();
        if (collider.gameObject.tag == "Enemys" && isDestroied == false)
        {
            collider.GetComponent<EnemyController>().TakeDamage(DamageToDeal);
            Destroy(gameObject);
            isDestroied = true;
        }
    }
}
