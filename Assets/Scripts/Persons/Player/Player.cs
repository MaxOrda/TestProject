using UnityEngine;
using UnityEngine.UI;

public sealed class Player : Person
{
    private Slider healthBar;

    private void Start()
    {
        healthBar = GameObject.Find("UI").GetComponentInChildren<Slider>(true);

        StartSetup();
    }

    void Update()
    {
        if (healthBar != null)
        {
            healthBar.value = 1.0f * CurrentHealth / MaxHealth;
        }
    }
}