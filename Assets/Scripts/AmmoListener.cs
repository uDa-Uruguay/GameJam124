using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoListener : MonoBehaviour
{
    private TextMeshProUGUI ammoText;
    private AudioSource _audio;

    [Header("No ammo animation")]
    [SerializeField] private Vector3 scale;
    [SerializeField] private float rotation;
    [SerializeField] private float rotationDuration;
    [SerializeField] private float rotationInterval;
    [SerializeField] private float time;
    [SerializeField] private float rotations;

    private void Start()
    {
        ammoText = this.GetComponent<TextMeshProUGUI>();
        _audio = this.GetComponent<AudioSource>();

        GameEvents.current.onWeaponChange += UpdateInfo;
        GameEvents.current.onNoMoreAmmo += NoAmmoAnimation;
        GameEvents.current.onAmmoCollected += NewAmmoAnimation;
    }

    private void UpdateInfo(int maxAmmo, int currentAmmo)
    {
        ammoText.text = $"Ammo: {currentAmmo}/{maxAmmo}";
    }

    // No ammo Animation
    private void NoAmmoAnimation()
    {
        ammoText.color = Color.red;
        StartCoroutine(NoAmmoAnimationCo());
    }

    private IEnumerator NoAmmoAnimationCo()
    {
        LeanTween.scale(this.gameObject, scale, time).setEaseInSine();
        for (int i = 0; i < rotations; i++)
        {
            LeanTween.rotateZ(this.gameObject, rotation, rotationDuration).setEaseInSine();
            yield return new WaitForSeconds(rotationInterval);
            LeanTween.rotateZ(this.gameObject, -rotation, rotationDuration).setEaseInSine();
            yield return new WaitForSeconds(rotationInterval);
        }
        LeanTween.rotateZ(this.gameObject, 0f, time).setEaseInSine();
        LeanTween.scale(this.gameObject, Vector3.one , time).setEaseInSine();
        ammoText.color = Color.white;
    }

    // Added ammo Animation

    private void NewAmmoAnimation()
    {
        ammoText.color = Color.yellow;
        StartCoroutine(NoAmmoAnimationCo());

        _audio.pitch = Random.Range(1f, 2f);
        _audio.Play();
    }

}
