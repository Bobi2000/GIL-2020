using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public void PressUpgradeButton()
    {
        TileController.Upgrade(1);
    }

    public void PressSellButton()
    {
        TileController.Upgrade(2);
    }
}
