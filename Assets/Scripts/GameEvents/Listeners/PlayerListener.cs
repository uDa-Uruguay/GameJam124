using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListener : MonoBehaviour
{
    [Header("Sprite color change")]
    [SerializeField] private GameObject spriteGO;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color hitColor;
    [SerializeField] private float timeHit;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip noAmmoSound;

    private void Start()
    {
        _spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();

        GameEvents.current.onPlayerTakingDamage += redSpriteActive;
        GameEvents.current.onPlayerTakingDamage += hitSoundPlay;

        GameEvents.current.onNoMoreAmmo += noAmmoSoundPlay;
    }

    // When hit events //

    // Change to color Red.
    private void redSpriteActive()
    {
        StartCoroutine(redSprite());
    }
    private IEnumerator redSprite()
    {
        _spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(timeHit);
        _spriteRenderer.color = Color.white;
    }

    // Sound effects
    private void hitSoundPlay()
    {
        audioSource.clip = hitSound;
        audioSource.pitch = Random.Range(1f, 2f);
        audioSource.Play();
    }
    private void noAmmoSoundPlay()
    {
        audioSource.clip = noAmmoSound;
        audioSource.Play();
    }


    private void OnDestroy()
    {
        GameEvents.current.onPlayerTakingDamage -= redSpriteActive;
        GameEvents.current.onPlayerTakingDamage -= hitSoundPlay;

        GameEvents.current.onNoMoreAmmo -= noAmmoSoundPlay;
    }
}
