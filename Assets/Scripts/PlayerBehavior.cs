using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float JumpChargeTime = 0;
    public float JumpVelocity = 5f;
    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    public ParticleSystem _ps;
    public float shieldTimer;
    public GameBehavior _gameManager;

    public AudioClip hurt_s;
    public AudioClip hurt;
    public AudioClip fr_get;
    public AudioClip fr_lose;
    public AudioClip die;
    public AudioClip jump;
    public AudioClip jumpL;


    private CapsuleCollider _col;
    private bool _isCharging;
    private bool _isJumping;
    private float _vInput;
    private float _hInput;
    private AudioSource _as;

    private Rigidbody _rb;

    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
        shieldTimer = _gameManager.ShieldTimer;
        _gameManager.Shield = 0;
        _gameManager.FireRate = false;
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        _isJumping |= Input.GetKeyUp(KeyCode.Space);
        _isCharging = Input.GetKey(KeyCode.Space);
    }
    
    void checkJump()
    {
        if (IsGrounded() && _isJumping)
        {
            if (JumpChargeTime < 1)
            {
                _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
                _as.PlayOneShot(jump);
            }
            else
            {
                if (JumpChargeTime > 1.5f)
                {
                    JumpChargeTime = 1.5f;
                }
                _rb.AddForce(Vector3.up * JumpVelocity * (JumpChargeTime * 2), ForceMode.Impulse);
                _rb.AddForce(this.transform.forward * JumpVelocity * JumpChargeTime, ForceMode.Impulse);
                _as.PlayOneShot(jumpL);
            }
            _ps.Play(true);

        }
        if (IsGrounded() && _isCharging)
        {
            JumpChargeTime += 0.03f;
        }
        else
        {
            JumpChargeTime = 0;
        }
        _isJumping = false;
    }

    void checkShield()
    {
        if (_gameManager.Shield < _gameManager.MaxShield)
        {
            if (shieldTimer > 0)
            {
                shieldTimer -= Time.deltaTime;
                _gameManager.ShieldTimer -= (Time.deltaTime);
            } 
            else
            {
                shieldTimer = 20f;
                _gameManager.ShieldTimer = shieldTimer;
                _gameManager.Shield = _gameManager.MaxShield;
            }
        }
    }
    void FixedUpdate()
    {
        checkJump();
        checkShield();
        Vector3 rotation = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string n = collision.gameObject.name;
        if(n == "Enemy" || n == "Enemy(Clone)" || n == "Enemy_Bullet(Clone)")
        {
            if(_gameManager.Shield != 0)
            {
                _gameManager.Shield -= 1;
                _as.PlayOneShot(hurt_s);
            }
            else
            {
                _gameManager.HP -= 1;
                _as.PlayOneShot(hurt);
            }
        }
        if (n == "Firerate_Pickup")
        {
            if (collision.gameObject.GetComponent<ItemBehavior>().itemSound == null)
            {
                Debug.LogError("Item Sound is NULL");
            }
            else
            {
                _as.PlayOneShot(collision.gameObject.GetComponent<ItemBehavior>().itemSound, 0.5f);
            }
        }
        if(n == "Health_Pickup" || n == "Shield_Pickup")
        {
            if (collision.gameObject.GetComponent<ItemBehavior>().itemSound == null)
            {
                Debug.LogError("Item Sound is NULL");
            }
            else
            {
                _as.PlayOneShot(collision.gameObject.GetComponent<ItemBehavior>().itemSound);
            }
            
        }
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }

}