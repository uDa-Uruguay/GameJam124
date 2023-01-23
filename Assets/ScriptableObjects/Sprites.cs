using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteContainer", menuName = "ScriptableObjects/SpritesContainer")]

public class Sprites : ScriptableObject
{
    [field: SerializeField] public Sprite[] SpriteList { get; private set; } // Sprites.

}
