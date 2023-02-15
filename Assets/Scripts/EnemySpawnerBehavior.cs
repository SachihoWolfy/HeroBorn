using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehavior : MonoBehaviour
{
    public bool canSpawn;
    public float _timer = 20f;
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        canSpawn = _timer <= 0;

        if (canSpawn)
        {
            GameObject newEnemy = Instantiate(Enemy, this.transform.position, this.transform.rotation);
            canSpawn = false;
            _timer = 15f;
        }
    }
}
