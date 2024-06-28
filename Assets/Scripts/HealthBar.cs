using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;
    public int maxHealth = 100;

    public void SetHealth(int health)
    {
        fill.fillAmount = (float) health / maxHealth;
    }
}
