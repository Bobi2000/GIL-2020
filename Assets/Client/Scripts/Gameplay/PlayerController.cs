using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public bool isPlayer = false;

    public string username;

    public TextMeshProUGUI usernameText;

    public float money = 100f;

    public int badLuck { get; private set; }

    public int kills { get; private set; }

    public TextMeshProUGUI goldText;

    private void Start()
    {
        var gm = GameObject.Find("TextGold");
        this.goldText = gm.GetComponent<TextMeshProUGUI>();

        this.ChangeTextValue(this.money);

        InvokeRepeating("GiveMoney", 8, 8);
    }

    public void GiveMoney()
    {
        this.AddGold(5);
    }

    private void Death()
    {
       // throw new NotImplementedException();
    }
    public void AddGold(float amount)
    {
        this.money += amount;
        this.ChangeTextValue(this.money);
    }
    public void SubtracGold(float amount)
    {
        if (money-amount<0)
        {
            this.money = 0;
            return;
        }
        this.money -= amount;
        this.ChangeTextValue(this.money);
    }

    public void SetName()
    {
        Debug.Log("setname");
        this.usernameText.text = this.username;
    }

    private void ChangeTextValue(float totallMoney)
    {
        this.goldText.text = $"{totallMoney:F2}";
    }
}
