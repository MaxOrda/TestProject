using System;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : Person
{
    private Text score;

    private GameObject player;

    private Rigidbody2D rb;

    private void Start()
    {
        score = GameObject.Find("UI").GetComponentInChildren<Text>(true);

        player = GameObject.Find("Player");

        rb = GetComponent<Rigidbody2D>();

        StartSetup();
    }

    public GameObject Player => player;

    public Rigidbody2D Rigidbody => rb;

    private void Update()
    {
        Move();

        Fire();
    }

    public abstract void Move();

    public virtual void Fire() { }

    private void OnDestroy()
    {
        if (score != null)
        {
            var scoreNumber = Int32.Parse(score.text.Substring(7)) + 1;

            score.text = "Kills: " + scoreNumber;
        }
    }
}
