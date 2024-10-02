using System;

using UnityEngine;

public abstract class Person : MonoBehaviour
{
    [SerializeField]
    private Int32 maxHealth = 100;

    private Int32 currentHealth;

    public void StartSetup()
    {
        currentHealth = maxHealth;
    }

    public Int32 CurrentHealth => currentHealth;

    public Int32 MaxHealth => maxHealth;

    public void ChangeHealth(Int32 deltaHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth + deltaHealth, 0, maxHealth);

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}