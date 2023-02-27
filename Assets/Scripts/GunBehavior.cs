using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(AudioSource))]
public class Gun_Script : MonoBehaviour
{
    public GameObject Bullet;
    public float BulletSpeed = 100f;
    public float shootTime = 2f;
    public float _timer = 2f;
    public bool canShoot;
    public GameBehavior GameManager;
    public TMP_Text GunStatus;
    public AudioSource sounds;
    public AudioClip rapidFire;
    public AudioClip normalFire;

    private float BuffTimer = 20f;
    private bool _firingRP = false;

    private bool _isShooting;
    void Start()
    {
        GameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
        BuffTimer = GameManager.FR_Buff_Time;
        sounds = GetComponent<AudioSource>();
        sounds.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        _isShooting |= (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.Return));
        canShoot = GameManager.FireRate || _timer <= 0;
        if (canShoot)
        {
            GunStatus.text = "GUN: READY";
        }
        else
        {
            GunStatus.text = "GUN: RECHARGING...";
        }

        if (_isShooting && GameManager.FireRate == true)
        {
            _firingRP = true;
        }

    }

    private void FixedUpdate()
    {
        if (GameManager.FireRate == true)
        {
            if(BuffTimer > 0)
            {
                BuffTimer -= Time.deltaTime;
            }
            else
            {
                GameManager.FireRate = false;
                BuffTimer = GameManager.FR_Buff_Time;
            }
        }
        if (_isShooting && canShoot)
        {
            GameObject newBullet = Instantiate(Bullet, this.transform.position, this.transform.rotation);
            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();
            BulletRB.velocity = this.transform.forward * BulletSpeed;
            if (GameManager.FireRate == true)
            {
                if (!sounds.isPlaying)
                {
                    sounds.Play(0);
                    _firingRP = true;
                }

            }
            else
            {
                sounds.PlayOneShot(normalFire);
            }
            canShoot = false;
            _timer = 2f;
        }
        if(_isShooting == false && _firingRP == true)
        {
            sounds.Stop();
            _firingRP = false;
        }
        _isShooting = false;
    }
}
