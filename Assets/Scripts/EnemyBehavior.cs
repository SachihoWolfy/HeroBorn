using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public AudioSource audioPlayer;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) 
    {
        if(other.name == "Player")
        {
            Debug.Log("STOP TOUCHING MEEEEEEEEEEEEEEEEE");
            audioPlayer.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Thank god...");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
