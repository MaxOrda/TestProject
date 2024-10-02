using System;
using System.Collections;

using UnityEngine;

public sealed class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private Single reloadTime = 2.0f;

    [SerializeField]
    private Single fireSpeed = 1.0f;

    [SerializeField]
    private Int32 clip = 30;

    private Int32 currentClip;

    private Boolean isReloading = false;
    private Boolean isFireCooldown = false;

    private void Start()
    {
        StartCoroutine(Reload());
    }

    private void OnEnable()
    {
        isReloading = false;
        isFireCooldown = false;
    }

    public void Shoot()
    {
        if (projectilePrefab != null && !isFireCooldown && !isReloading)
        {
            currentClip = Mathf.Clamp(currentClip - 1, 0, clip);

            var projectile = Instantiate(projectilePrefab);

            projectile.transform.rotation = transform.rotation;

            projectile.transform.position = transform.position + (transform.rotation * Vector3.right) * 2;

            if (currentClip == 0)
            {
                StartCoroutine(Reload());
            }

            StartCoroutine(FireCooldown());
        }
    }

    public IEnumerator ChangeFireSpeed(Single deltaFireSpeed, Single boostTime)
    {
        fireSpeed += deltaFireSpeed;

        yield return new WaitForSeconds(boostTime);

        fireSpeed -= deltaFireSpeed;
    }

    public IEnumerator ChangeClip(Int32 deltaClip, Single boostTime)
    {
        clip += deltaClip;

        yield return new WaitForSeconds(boostTime);

        clip -= deltaClip;
    }

    public IEnumerator FireCooldown()
    {
        isFireCooldown = true;

        yield return new WaitForSeconds(1 / fireSpeed);

        isFireCooldown = false;
    }

    public IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentClip = clip;

        isReloading = false;
    }
}