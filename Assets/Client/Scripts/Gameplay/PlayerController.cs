using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isPlayer = false;

    public string username;

    public float money { get; private set; }

    public int badLuck { get; private set; }

    public int kills { get; private set; }

    public TextMeshProUGUI usernameText;

    private void Death()
    {
        throw new NotImplementedException();
    }

    public void AddGold(float amount)
    {
        this.money += amount;
    }

    public void SubtracGold(float amount)
    {
        if (money-amount<0)
        {
            this.money = 0;
            return;
        }
        this.money -= amount;
    }

    public void ChangeName()
    {
        Debug.Log("Changed name");
        this.usernameText.text = this.username;
    }
}
