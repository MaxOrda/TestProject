using System;

using UnityEngine;

public enum Owner
{
    Player = 0,
    Enemy  = 1,
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public sealed class Projectile : MonoBehaviour
{
    [SerializeField]
    private Owner owner = Owner.Player;

    [SerializeField]
    private Int32 damage = 1;

    [SerializeField]
    private Single startForce = 1.0f;

    private void Start()
    {
        SetupIgnoringColliders();

        var rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.rotation * Vector2.right * startForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;

        if (tag == "Player" || tag == "Enemy")
        {
            var person = collision.gameObject.GetComponent<Person>();

            person.ChangeHealth(-damage);
        }

        Destroy(gameObject);
    }

    private void SetupIgnoringColliders()
    {
        var projectileCollider = GetComponent<Collider2D>();

        var projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject projectile in projectiles)
        {
            if (owner == projectile.GetComponent<Projectile>().owner)
            {
                var otherProjectileCollider = projectile.GetComponent<Collider2D>();

                Physics2D.IgnoreCollision(projectileCollider, otherProjectileCollider, true);
            }
        }

        if (owner == Owner.Enemy)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                var enemyCollider = enemy.GetComponent<Collider2D>();

                Physics2D.IgnoreCollision(enemyCollider, projectileCollider, true);
            }
        }
        else
        {
            var playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();

            Physics2D.IgnoreCollision(playerCollider, projectileCollider, true);
        }
    }
}
