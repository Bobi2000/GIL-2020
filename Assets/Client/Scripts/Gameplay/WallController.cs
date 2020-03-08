using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public float MaxHealth=1000;
    public float CurrentHealth=1000;
    public float NextUpgradeCost = 100f;
    public float TotalCost = 10f;

    public float sellCoef = 0.6f;
    public float repairCoef = 0.4f;

    RandomGenerator generator = new RandomGenerator();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Upgrade()
    {
        var UpdateStats = generator.RandomTurretStatsOnUpdate(ClientController.playerController.badLuck);
        this.MaxHealth += UpdateStats[0];
             
    }
    public void DealWallDamage(float amount)
    {
       // this.CurrentHealth -= amount-5;

    }
    public void Sell()
    {
        ClientController.playerController.AddGold(sellCoef * TotalCost);

    }

    private void Repair()
    {
        ClientController.playerController.SubtracGold(repairCoef * TotalCost - 10 * CurrentHealth);
        this.CurrentHealth = this.MaxHealth;
    }
}
