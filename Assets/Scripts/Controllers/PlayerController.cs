using System;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Single speed = 5.0f;

    [SerializeField]
    private Single jumpForce = 10.0f;

    private Gun mainWeapon;

    private Gun alternativeWeapon;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        var weapons = GetComponentsInChildren<Gun>(true);

        mainWeapon = weapons[0];

        alternativeWeapon = weapons[1];
    }

    void Update()
    {
        Move();
        
        Jumping();

        RotationMainWeapon();

        Fire();
    }

    private void Move()
    {
        var xMove = Input.GetAxisRaw("Horizontal");

        transform.position += Vector3.right * speed * xMove * Time.deltaTime;
    }

    private void Jumping()
    {
        if (Input.GetKey(KeyCode.Space) && rb.velocity.y == 0.0f)
        {
            rb.velocity = new Vector2(0.0f, jumpForce);
        }
    }

    private void RotationMainWeapon()
    {
        if (mainWeapon != null)
        {
            var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            var rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            if (-90 < rotateZ && rotateZ < 0) rotateZ = 0;

            if (-180 < rotateZ && rotateZ < -90) rotateZ = 180;

            mainWeapon.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotateZ);
        }

        if (alternativeWeapon != null)
        {
            var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            var rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            if (-90 < rotateZ && rotateZ < 0) rotateZ = 0;

            if (-180 < rotateZ && rotateZ < -90) rotateZ = 180;

            alternativeWeapon.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotateZ);
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButton(0) && mainWeapon != null)
        {
            mainWeapon.Shoot();
        }

        if (Input.GetMouseButton(1) && alternativeWeapon != null)
        {
            alternativeWeapon.Shoot();
        }
    }
}