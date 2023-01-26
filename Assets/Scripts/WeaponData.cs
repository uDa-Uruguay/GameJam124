using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{

    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject bullet;
    public Transform bulletTranform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    [SerializeField] AudioSource _audio;

    [Header("Bullets info")]
    [SerializeField] public float force;
    [SerializeField] public float damage;
    [SerializeField] public int maxAmmo;
    [SerializeField] public int currentAmmo;


    void OnEnable()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        //Investigar despues esta linea de codigo
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //Disparo

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            if (currentAmmo > 0)
            {
                Instantiate(bullet, bulletTranform.position, Quaternion.identity);
                _audio.pitch = Random.Range(1f, 2f);
                _audio.Play();
                currentAmmo -= 1;
                GameEvents.current.WeaponChange(maxAmmo, currentAmmo);
            } else GameEvents.current.NoMoreAmmo();
        }

    }
}
