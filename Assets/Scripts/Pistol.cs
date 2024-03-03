using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Pistol : MonoBehaviour
{
    public Transform holsterLocation;
    private Animator gunAnimator;
    public GameObject bullet;
    public Transform barrelLocation;
    private float bulletSpeed =10000f;
    private bool isGrabbed;
    private float maxAmmo = 10;
    private float shotAmmo;
    private XRGrabInteractable grabbable;
    private Text ammoNumText;
    private Canvas gunCanvas;
    private AudioSource gunSource;
    public List<AudioClip> gunSounds;

    // Start is called before the first frame update
    void Start()
    {
       
        SetEvents();
        GunToHolster();
        SetValues();
        changeAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
     if(!isGrabbed)
        {
            GunToHolster();
        }
    }

    public void GunToHolster()
    {
        transform.position = holsterLocation.position;
        transform.rotation = holsterLocation.rotation;
    }
  
    public void OnShoot(ActivateEventArgs args)
    {
        FireGun();
    }
  
    private void SetEvents()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(OnShoot);
     }
    private void SetValues()
    {
        gunAnimator = GetComponentInChildren<Animator>();
        isGrabbed = false;
        shotAmmo = 0;
        ammoNumText = GameObject.Find("Ammo Number Text").GetComponent<Text>();
        gunCanvas = GetComponentInChildren<Canvas>();
        gunCanvas.enabled = false;
        gunSource = GetComponent<AudioSource>();
    }
    public void SetGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        changeAmmoText();
        gunCanvas.enabled = true;
    }
    public void UnsetGrab(SelectExitEventArgs args)
    {
        isGrabbed = false;
        grabbable.enabled = false;
        StartCoroutine(ReloadPistolAfterTime());
        gunCanvas.enabled = false;
    }

    private bool EnoughAmmo()
    {
        if (shotAmmo < maxAmmo)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void FireGun()
    {
        if (EnoughAmmo())
        {
            //shoot bullet
            GameObject bulletClone = Instantiate(bullet, barrelLocation.position, barrelLocation.rotation);
            bulletClone.GetComponent<Rigidbody>().velocity = barrelLocation.forward * bulletSpeed * Time.deltaTime;
            gunAnimator.SetTrigger("Fire");
            //
            shotAmmo++;
            changeAmmoText();
            gunSource.clip = gunSounds[0];
            gunSource.Play();
        }
        else
        {
            gunSource.clip = gunSounds[1];
            gunSource.Play();
        }
    }
    IEnumerator ReloadPistolAfterTime()
    {
        yield return new WaitForSeconds(5);
        shotAmmo = 0;
        grabbable.enabled = true;
    }

    private void changeAmmoText()
    {
        
        ammoNumText.text = (maxAmmo - shotAmmo).ToString();
    }
}
