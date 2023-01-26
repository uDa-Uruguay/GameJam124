using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCointainerManager : MonoBehaviour
{
    [SerializeField] public Sprites container;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int lastSpriteSet = 0;

    public void setNewSprite(int i)
    {
        spriteRenderer.sprite = container.SpriteList[i];
        lastSpriteSet = i;
    }
}
