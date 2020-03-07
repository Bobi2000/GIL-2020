
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    public float MaxHealth;
    public float CurrentHealth;
    public float Damage;
    public float Range;
    public float NextUpgradeCost = 100f;
    public float TotalCost = 10f;

    public float sellCoef = 0.6f;
    public float repairCoef = 0.4f;

    public GameObject range;
    PlayerController player= new PlayerController();

    RandomGenerator generator=new RandomGenerator();   
    void Start()
    {
        range.GetComponent<CircleCollider2D>().radius = this.Range/10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Upgrade();
        }
    }

    private void Build()
    {
        throw new NotImplementedException();
    }

    private void Upgrade()
    {
        var UpdateStats = generator.RandomTurretStatsOnUpdate(player.badLuck);
        this.MaxHealth += UpdateStats[0];
        this.Damage += UpdateStats[1];
        this.Range += UpdateStats[2];
        range.GetComponent<CircleCollider2D>().radius = this.Range/10;
        var avatageUpgrade = (UpdateStats[0] + UpdateStats[1] + UpdateStats[2]) / 3;
        NextUpgradeCost += avatageUpgrade*10;
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
}
