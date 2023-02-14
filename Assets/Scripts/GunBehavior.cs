using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{
    public GameObject Bullet;
    public float BulletSpeed = 100f;

    private bool _isShooting;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _isShooting |= Input.GetKeyDown(KeyCode.RightShift);
    }

    private void FixedUpdate()
    {
        if (_isShooting)
        {
            GameObject newBullet = Instantiate(Bullet, this.transform.position, this.transform.rotation);
            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();
            BulletRB.velocity = this.transform.forward * BulletSpeed;
        }
        _isShooting = false;
    }
}
