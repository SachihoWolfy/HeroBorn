using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunBehavior : MonoBehaviour
{
    public GameObject Enemy_Bullet;
    public float BulletSpeed = 100f;
    public float shootTime = 2f;
    public float _timer = 2f;
    public bool canShoot;

    private float BuffTimer = 20f;

    private bool _isShooting;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        _isShooting = true;
        canShoot = _timer <= 0;
    }

    private void FixedUpdate()
    {
        if (_isShooting && canShoot)
        {
            GameObject newBullet = Instantiate(Enemy_Bullet, this.transform.position, this.transform.rotation);
            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();
            BulletRB.velocity = this.transform.forward * BulletSpeed;
            canShoot = false;
            _timer = 4f;
        }
        _isShooting = false;
    }
}