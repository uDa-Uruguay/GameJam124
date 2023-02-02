using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{

    private Camera mainCam;
    [SerializeField] public bool isChargable;

    [SerializeField] private Rigidbody2D bullet;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    private AudioSource _audioSource;

    [Header("Bullets info")]
    [SerializeField] public float force;
    [SerializeField] public float damage;
    [SerializeField] public int maxAmmo;
    [SerializeField] public int currentAmmo;

    [Header("Click hold control")]
    [SerializeField] private float defaultForce; // Default force applied to the arrow
    [SerializeField] private float maxForce = 20f; // Maximum force applied to the arrow
    [SerializeField] private float increaseInterval = 0.5f; // The interval between increases in force (In seconds)

    // Flags
    [HideInInspector]public bool holding = false; // Flag to indicate if the right mouse button is held down
    private bool increasing = false; // Flag to indicate if the coroutine is running


    void OnEnable()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _audioSource = GetComponent<AudioSource>();

        force = defaultForce;
    }

    void Update()
    {
        if (ShopButtonManager.isShopOpen) return;

        if(!_audioSource) _audioSource = GetComponent<AudioSource>();

        MoveWithMouse();

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

        if (isChargable) ChargableWeaponBehavior();
        else NotChargableBehavior();
        
    }

    private void MoveWithMouse(){ // Moves the weapon accordingly to the direction where the mouse is pointing.
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; //Rotacion en degrees.
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void Shoot(){
        // Determine the direction of the shot based on the mouse position
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (worldMousePosition - transform.position).normalized;

        // Create an instance of the arrow prefab at the player's position
        Rigidbody2D arrowInstance = Instantiate(bullet, transform.position, Quaternion.identity);

        // Setting the correct angle based
        float angle = Vector2.SignedAngle(arrowInstance.transform.up, direction); 
        arrowInstance.transform.Rotate(0, 0, angle);

        // Apply force to the arrow in the direction of the shot
        arrowInstance.AddForce(arrowInstance.transform.up * force, ForceMode2D.Impulse);
        
        // Reset the force applied to the arrow
        force = defaultForce;
    }

    // Chargable behavior
    private void ChargableWeaponBehavior(){
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && canFire)
        {
            holding = true;
            canFire = false;
            if (currentAmmo > 0 && !increasing)
            {
                StartCoroutine(IncreaseForce());
                increasing = true;

                // Instantiate(bullet, bulletTranform.position, Quaternion.identity);
            } else GameEvents.current.NoMoreAmmo();
        } else if ((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)) && currentAmmo > 0 && holding) {
            // Here's when the arrow gets shoot and everthing comes back to default
            holding = false;
            increasing = false;
            Shoot();
            SideEffects();
        }
    }

    IEnumerator IncreaseForce(){
        while (holding) // Using while instead of if creates a loop. In this case, the force gets up while you still holding.
        {
            // Wait for the increaseInterval time before increasing the force the first time and then again
            yield return new WaitForSeconds(increaseInterval);
            // Increase the force applied to the arrow, but keep it within the maxForce limit
            if (holding) force = Mathf.Min(force + 1f, maxForce);
        }
        // When the left mouse button is released, set the increasing flag to false
        increasing = false;
    }

    // NOT Chargable behavior

    private void NotChargableBehavior(){
        if((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && canFire && currentAmmo > 0){
            canFire = false;
            Shoot();
            SideEffects();
        }
    }

    private void SideEffects(){
        // Sound effects
        _audioSource.pitch = Random.Range(1f, 2f);
        _audioSource.Play();
        
        currentAmmo -= 1; // Ammo update

        // Event occur
        GameEvents.current.WeaponChange(maxAmmo, currentAmmo);
    }
}

