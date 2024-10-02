using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class MediumEnemy : Enemy
{
    [SerializeField]
    private Single speed;

    [SerializeField]
    private Single radius;

    private Vector3 center;

    private Single angle = 0.0f;

    private Gun gun;

    private void Awake()
    {
        center = transform.position;

        gun = GetComponentInChildren<Gun>(true);
    }

    public override void Move()
    {
        angle += Time.deltaTime;

        var x = radius * Mathf.Cos(angle * speed) + center.x;
        var y = radius * Mathf.Sin(angle * speed) + center.y;

        transform.position = new Vector3(x, y, 0.0f);

        RotateWeaponToPlayer();
    }

    private void RotateWeaponToPlayer()
    {
        if (gun != null)
        {
            var difference = (Player.transform.position - transform.position);

            var rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            gun.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotateZ);
        }
    }

    public override void Fire()
    {
        var direction = (Player.transform.position - transform.position).normalized;

        var hit = Physics2D.Raycast(transform.position, direction);

        if (hit)
        {
            if (hit.collider.gameObject == Player)
            {
                gun?.Shoot();
            }
        }
    }
}
