using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehavior : MonoBehaviour
{
    public AudioSource audioSource;

    private bool _isMuting;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _isMuting |= Input.GetKeyDown(KeyCode.M);
    }

    private void FixedUpdate()
    {
        if (_isMuting)
        {
            audioSource.mute = !audioSource.mute;
        }
        _isMuting = false;
        
    }
}
