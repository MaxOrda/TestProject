using System;

using UnityEngine;

public sealed class LightEnemy : Enemy
{
    [SerializeField]
    private Single boostForce;

    public override void Move()
    {
        if (Player != null)
        {
            Rigidbody.velocity = (Player.transform.position - transform.position) * boostForce;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().ChangeHealth(-1);
        }
    }
}
