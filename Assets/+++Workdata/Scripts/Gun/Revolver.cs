using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Gun
{
    Camera playerCamera;
    Vector3 originalPos;

    public int projectileAmount;


    public void Start()
    {
        playerCamera = Camera.main;
        originalPos = playerCamera.transform.localPosition;
        currentAmmoAmount = ammoAmount;
    }

    private void Update()
    {
        DetermineAmmo();
    }

    public void DetermineAmmo()
    {
        if (currentAmmoAmount <= 0)
        {
            currentAmmoAmount = 0;
            if (!isReloading)
            {
                StartCoroutine(Reload());
                print(currentAmmoAmount);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            projectileForce = firstProjectileType.GetComponent<Projectile>().projectileStats.projectileForce;
            HandleShooting(firstProjectileType);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            projectileForce = secondaryProjectileType.GetComponent<Projectile>().projectileStats.projectileForce;
            HandleShooting(secondaryProjectileType);
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
            for (int i = 0; i < projectileAmount; i++)
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
        }

        if (currentAmmoAmount == 0) return;
        currentAmmoAmount--;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        currentAmmoAmount = ammoAmount;
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
