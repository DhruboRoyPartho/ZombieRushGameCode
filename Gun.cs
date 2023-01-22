using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Text ammoText;

    public AudioClip Reload;
    public AudioClip GunShootClip;
    public AudioSource source;

    public Camera fpsCam;

    public Animator gunAnim;

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 4f;

    public float impactForce = 100f;

    public GameObject muzzleFlash;
    public GameObject muzzlePosition;

    private float nextTimeToFire = 0f;

    private string RUN = "isRun";
    private string SHOOT = "Shoot";

    public float X,Z;

    public float maxAmmo=10f;
    public float currentAmmo;
    private float reloadTime = 2f;
    private bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        gunAnim = GetComponent<Animator>();
        // if(source != null){
        //     source.clip = GunShootClip;

        // }
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {


        X = Input.GetAxis("Horizontal");
        Z = Input.GetAxis("Vertical"); 

        ammoText.text = currentAmmo.ToString("0");

        if(isReloading)
            return;

        if(currentAmmo <= 0f || Input.GetKeyDown(KeyCode.R)){
            
            StartCoroutine(reload());
            
            return;
        }

        gunAnimation();

        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1/fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        currentAmmo--;
        if(currentAmmo >= 0f){
            var GO = Instantiate(muzzleFlash, muzzlePosition.transform);
            Destroy(GO, 1f);
        }
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);
            if(hit.transform.GetComponent<zombieScript>() != null)
                hit.transform.GetComponent<zombieScript>().takeDamage(damage);
        }
        if(source != null){
            source.clip = GunShootClip;
            source.Play();
        }
        if(hit.rigidbody != null){
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }
    }

    void gunAnimation()
    {
        if(Input.GetButton("Fire1")){
            gunAnim.SetTrigger(SHOOT);
        }
        if(X != 0f || Z != 0f){
            gunAnim.SetBool(RUN, true);
        }
        if(X ==  0 && Z == 0){
            gunAnim.SetBool(RUN, false);
        }
    }

    IEnumerator reload()
    {
        isReloading = true;
        Debug.Log("Reloading");
        gunAnim.SetBool("isReload", true);

        if(source != null){
            source.clip = Reload;
        }
        source.Play();
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        gunAnim.SetBool("isReload", false);
    }
}
