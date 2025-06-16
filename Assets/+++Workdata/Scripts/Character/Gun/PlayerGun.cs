using System.Collections;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    Camera playerCamera;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] float projectileForce;
    Vector3 originalPos;


    public void Start()
    {
        playerCamera = Camera.main;
        originalPos = playerCamera.transform.localPosition;
    }

    private void Update()
    {
        HandleShooting();
    }

    public void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
            Vector3 shootDirection = ray.direction;

            GameObject projectile = Instantiate(
                projectilePrefab,
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
            yield return null; // Wait for the next frame
        }

        playerCamera.transform.localPosition = originalPos; // Reset to original position
    }
}
