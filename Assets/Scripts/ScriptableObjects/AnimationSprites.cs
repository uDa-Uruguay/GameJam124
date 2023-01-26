using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationSpriteContainer", menuName = "ScriptableObjects/AnimationSpriteContainer")]

public class AnimationSprites : ScriptableObject
{
    [field: SerializeField] public Sprite[] SpriteList { get; private set; } // Sprites.
    [field: SerializeField] public int FramesPerSecond { get; private set; } // Frames per second. 

}
