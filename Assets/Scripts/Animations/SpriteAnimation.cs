using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] AnimationSprites spritesContainer;
    private Sprite[] _animation;
    private int _framesPerSecond;

    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        _animation = spritesContainer.SpriteList;
        _framesPerSecond = spritesContainer.FramesPerSecond;

        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if (!_spriteRenderer) Debug.Log("Sprite Renderer not found in GameObject.");

        StartCoroutine(startAnimation());
    }


    private IEnumerator startAnimation()
    {
        for (int i = 0; i < _animation.Length; i++)
        {
            _spriteRenderer.sprite = _animation[i];
            yield return new WaitForSeconds(1f / _framesPerSecond);
        }

        StartCoroutine(startAnimation());
    }

}
