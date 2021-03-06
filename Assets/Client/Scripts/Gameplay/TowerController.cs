﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots = 2f;

    public bool IsTurret;

    public float MaxHealth;
    public float CurrentHealth;
    public float Damage;
    public float Range;
    public float NextUpgradeCost = 100f;
    public float TotalCost = 10f;

    public float sellCoef = 0.6f;
    public float repairCoef = 0.4f;

    public float SellPrice;
    public GameObject bullet;
   

    private GameObject Enemy;

    RandomGenerator generator = new RandomGenerator();
    void Start()
    {
        SellPrice = TotalCost * sellCoef;
        // GetComponent<CircleCollider2D>().radius = this.Range / 10;
        //var randomStats=generator.RandomTurretStatsOnCreate(player.badLuck);
        //this.MaxHealth = this.CurrentHealth = randomStats[0];
        //this.Damage = randomStats[1];
        //this.Range = randomStats[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (this.CurrentHealth<=0)
        {
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown("space"))
        {
            Upgrade();
        }

        if (timeBtwShots <= 0 && Enemy != null&&IsTurret)
        {

            Shoot(Enemy);
            timeBtwShots = startTimeBtwShots;

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }


    public void Upgrade()
    {
        if (ClientController.playerController.money - NextUpgradeCost >= 0)
        {
            var UpdateStats = generator.RandomTurretStatsOnUpdate(ClientController.playerController.badLuck);
            this.MaxHealth += UpdateStats[0];
            this.Damage += UpdateStats[1];
            ClientController.playerController.SubtracGold(NextUpgradeCost);
            GetComponent<CircleCollider2D>().radius = this.Range / 10;
            var avarageUpgrade = (UpdateStats[0] + UpdateStats[1] + UpdateStats[2]) / 3;
            NextUpgradeCost += avarageUpgrade * 10;
            SellPrice = NextUpgradeCost * sellCoef;
            var logUpgrade = $"{Damage} {Range } ";
        }
    }

    public void Sell()
    {      
        ClientController.playerController.AddGold(SellPrice);
        Destroy(this.gameObject);
        
    }

    private void Repair()
    {
        ClientController.playerController.SubtracGold(repairCoef * TotalCost - 10 * CurrentHealth);
        this.CurrentHealth = MaxHealth;
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
        if (enemyToShoot!=null)
        {
        var currenBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
        currenBullet.GetComponent<BulletController>().ShootEnemy(enemyToShoot,Damage);
        }
    }
    public void DealTurretDamage(float amount)
    {
        this.CurrentHealth -= amount;
        
    }
}
