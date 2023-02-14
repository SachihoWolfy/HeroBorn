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

    private CapsuleCollider _col;
    private bool _isCharging;
    private bool _isJumping;
    private float _vInput;
    private float _hInput;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        _isJumping |= Input.GetKeyUp(KeyCode.Space);
        _isCharging = Input.GetKey(KeyCode.Space);
    }
    
    void FixedUpdate()
    {

        if (IsGrounded() && _isJumping)
        {
            if( JumpChargeTime < 1)
            {
                _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
            }
            else
            { 
            if(JumpChargeTime > 1.5f)
            {
                JumpChargeTime = 1.5f;
            }
                _rb.AddForce(Vector3.up * JumpVelocity * (JumpChargeTime * 2), ForceMode.Impulse);
                _rb.AddForce(this.transform.forward * JumpVelocity * JumpChargeTime, ForceMode.Impulse);
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
        Vector3 rotation = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }

}