
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots = 2f;

    public float MaxHealth;
    public float CurrentHealth;
    public float Damage;
    public float Range;
    public float NextUpgradeCost = 100f;
    public float TotalCost = 10f;

    public float sellCoef = 0.6f;
    public float repairCoef = 0.4f;

    //public CircleCollider2D BulletMelle;

    public GameObject bullet;

    public PlayerController player = ClientController.playerController;

    //PlayerController player = new PlayerController();

    private GameObject Enemy;

    RandomGenerator generator = new RandomGenerator();
    void Start()
    {
        // GetComponent<CircleCollider2D>().radius = this.Range / 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Upgrade();
        }

        if (timeBtwShots <= 0 && Enemy != null)
        {
            Debug.Log(timeBtwShots);
            Shoot(Enemy);
            timeBtwShots = startTimeBtwShots;

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }


    private void Upgrade()
    {
        var UpdateStats = generator.RandomTurretStatsOnUpdate(player.badLuck);
        this.MaxHealth += UpdateStats[0];
        this.Damage += UpdateStats[1];
        //  this.Range += UpdateStats[2];
        GetComponent<CircleCollider2D>().radius = this.Range / 10;
        var avatageUpgrade = (UpdateStats[0] + UpdateStats[1] + UpdateStats[2]) / 3;
        NextUpgradeCost += avatageUpgrade * 10;
        var logUpgrade = $"{Damage} {Range } ";
        Debug.Log(logUpgrade);
    }

    private void Sell()
    {
        this.player.AddGold(sellCoef * TotalCost);
    }

    private void Repair()
    {
        this.player.SubtracGold(repairCoef * TotalCost - 10 * CurrentHealth);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemys")
        {
            Enemy = collider.gameObject;
        }
    }
    private void Shoot(GameObject enemyToShoot)
    {
        var currenBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
        currenBullet.GetComponent<BulletController>().ShootEnemy(enemyToShoot);
    }
    public void DealTurretDamage(float amount)
    {
        this.CurrentHealth -= amount;
        if (this.CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
