using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] Shotgun shotgun;
    [SerializeField] Revolver revolver;

    [SerializeField] AmmoUI ammoUi;

    public GameObject firstAmmoType;
    public GameObject secondAmmoType;

    private void Start()
    {
        revolver.firstProjectileType = firstAmmoType;
        revolver.secondaryProjectileType = secondAmmoType;

        shotgun.firstProjectileType = firstAmmoType;
        shotgun.secondaryProjectileType = secondAmmoType;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            revolver.gameObject.SetActive(true);
            shotgun.gameObject.SetActive(false);

            ammoUi.gun = revolver;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            revolver.gameObject.SetActive(false);
            shotgun.gameObject.SetActive(true);

            ammoUi.gun = shotgun;
        }
    }
}
