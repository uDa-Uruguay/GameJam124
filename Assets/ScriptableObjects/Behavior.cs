using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sirve básicamente como contenedor para los behaviors o comportamientos. Util para ir configurando las ias.
public abstract class Behavior : ScriptableObject
{
    public abstract void behavior(EnemyData enemy);
}
