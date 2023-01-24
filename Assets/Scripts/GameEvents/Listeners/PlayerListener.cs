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

    private void Start()
    {
        _spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();

        GameEvents.current.onPlayerTakingDamage += redSpriteActive;
        GameEvents.current.onPlayerTakingDamage += hitSound;
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

    private void hitSound()
    {
        audioSource.Play();
    }
}
