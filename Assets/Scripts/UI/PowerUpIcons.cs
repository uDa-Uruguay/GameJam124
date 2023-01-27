using UnityEngine;

public class PowerUpIcons : MonoBehaviour
{
    [SerializeField] GameObject dashIcon;

    private void Update()
    {
        if (CurrentStats.current.haveDash) dashIcon.SetActive(true);
    }
}
