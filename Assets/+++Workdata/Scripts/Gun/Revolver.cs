using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Gun
{
    Camera playerCamera;
    Vector3 originalPos;

    public void Start()
    {
        playerCamera = Camera.main;
        originalPos = playerCamera.transform.localPosition;
        currentAmmoAmount = ammoAmount;
    }

    private void OnEnable()
    {
        ResetValues();
    }

    public IEnumerator DetermineAmmo()
    {
        while (true)
        {
            if (currentAmmoAmount <= 0)
            {
                currentAmmoAmount = 0;
                if (!isReloading)
                {
                    StartCoroutine(Reload());
                }
            }

            if (Input.GetMouseButton(0))
            {
                projectileForce = firstProjectileType.GetComponent<Bullet>().bulletSpeed;
                HandleShooting(firstProjectileType);
                yield return new WaitForSeconds(shootSpeed);

            }
            else if (Input.GetMouseButton(1))
            {
                projectileForce = secondaryProjectileType.GetComponent<Bullet>().bulletSpeed;
                HandleShooting(secondaryProjectileType);
                yield return new WaitForSeconds(shootSpeed);

            }

            yield return null;
        }
    }

    public void HandleShooting(GameObject ammoType)
    {
        if (currentAmmoAmount <= 0)
        {
            if (!isReloading)
            {
                //StartCoroutine(Reload());
                //print(currentAmmoAmount);
            }
        }
        else
        {
            print(ammoType.name);
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
            Vector3 shootDirection = ray.direction + Random.insideUnitSphere * bulletSpread;

            GameObject projectile = Instantiate(
                ammoType,
                playerCamera.transform.position + ray.direction.normalized * 2f,
                Quaternion.LookRotation(shootDirection)
            );

            particleSystem.Play();
            StartCoroutine(CameraShake());

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForce(shootDirection * projectileForce);
            }
        }

        if (currentAmmoAmount == 0) return;
        currentAmmoAmount--;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        ResetValues();
    }

    public void ResetValues()
    {
        currentAmmoAmount = ammoAmount;
        isReloading = false;
        StopAllCoroutines();
        StartCoroutine(DetermineAmmo());
    }

    IEnumerator CameraShake()
    {
        float elapsed = 0f;
        float shakeAmount = 0.2f;
        float shakeDuration = 0.05f;

        while (elapsed < shakeDuration)
        {
            Vector3 randomPoint = originalPos + Random.insideUnitSphere * shakeAmount;
            playerCamera.transform.localPosition = randomPoint;
            elapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.transform.localPosition = originalPos;
    }
}
