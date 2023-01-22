using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootgun : MonoBehaviour
{

    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject bullet;
    public Transform bulletTranform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    private float coneSize = 20f;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
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
            Vector2 spread = new Vector2(20f, 20f).normalized * coneSize;
            Quaternion Srotation = Quaternion.Euler(spread) * bulletTranform.rotation;

            canFire = false;
            Instantiate(bullet, bulletTranform.position, Quaternion.Euler(new Vector3(0f, 20f, 0)));
            Instantiate(bullet, bulletTranform.position, Quaternion.Euler(new Vector3(0f, 30f, 0)));
            Instantiate(bullet, bulletTranform.position, Quaternion.Euler(new Vector3(0f, -30f, 0)));
            Instantiate(bullet, bulletTranform.position, Quaternion.Euler(new Vector3(0f, -20f, 0)));

        }

    }
}
