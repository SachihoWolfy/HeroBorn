using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform Player;
    public AudioSource audioPlayer;
    public Transform PatrolRoute;
    public List<Transform> Locations;
    public bool _triggered = false;

    public GameBehavior _gameManager;

    private int _lives = 3;
    
    private Rigidbody _rb;

    private int _locationIndex = 0;
    private NavMeshAgent _agent;

    public int EnemyLives
    {
        get { return _lives; }
        private set
        {
            _lives = value;
            if(_lives <= 0)
            {
                if(_agent != null)
                {
                    Destroy(_agent);
                }
                
                _rb.constraints = RigidbodyConstraints.None;
                Invoke("enemyDestroy", 3 );

            }
        }
    }
    public void enemyDestroy()
    {
        Destroy(this.gameObject);
        Debug.Log("Enemy down");
        _gameManager.EnemiesDefeated += 1;
    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) 
    {
        if(other.name == "Player")
        {
            if(_agent != null)
            {
                _agent.destination = Player.position;
            }
            _triggered = true;
            Debug.Log("Detected");
            audioPlayer.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Not touched");
        }
    }
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player").transform;
        PatrolRoute = GameObject.Find("Patrol_Route").transform;
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("HIT!");
        }
    }

    void InitializePatrolRoute()
    {
        foreach(Transform child in PatrolRoute)
        {
            Locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (Locations.Count == 0)
            return;

        _agent.destination = Locations[_locationIndex].position;
        _locationIndex = (_locationIndex + 1) % Locations.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(_agent != null)
        {
            if (_agent.remainingDistance < 0.2f && !_agent.pathPending && !_triggered)
            {
                MoveToNextPatrolLocation();
            }
            else
                _agent.destination = Player.position;
        }
        
    }
}
