using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Image progressBar;

    public Gun gun;

    bool progressBarFill = false;

    private void Update()
    {
        UpdateCurrentAmmo();

        if (gun.currentAmmoAmount == 0 && gun.isReloading && !progressBarFill)
        {
            StartCoroutine(fillProgressBar());
        }
    }

    public void UpdateCurrentAmmo()
    {
        ammoText.text = $"{gun.currentAmmoAmount}/{gun.ammoAmount}";
    }

    IEnumerator fillProgressBar()
    {
        progressBarFill = true;
        float elapsed = 0f;
        float normalizedTime = 0f;

        progressBar.fillAmount = 0f;
        progressBar.color = Color.HSVToRGB(0.51f, 1f, .1f);

        while (elapsed < gun.reloadTime)
        {
            elapsed += Time.deltaTime;
            normalizedTime = Mathf.Clamp01(elapsed / gun.reloadTime);
            progressBar.color = Color.HSVToRGB(.51f, 1f, normalizedTime);
            progressBar.fillAmount = normalizedTime;
            yield return null;
        }

        progressBar.fillAmount = 1f;
        progressBar.color = Color.HSVToRGB(.51f, 1f, 1f);
        progressBarFill = false;
    }

}
