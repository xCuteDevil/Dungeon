using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        healthSystem.onHealthChanged += HealthSystem_OnHealthChange;
    }

    private void HealthSystem_OnHealthChange(object Sender, System.EventArgs e)
    {
        transform.Find("HealthBar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }
}
