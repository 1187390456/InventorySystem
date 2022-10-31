using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb;
    private Animator _at;

    private int faceDirection;

    private float _inputX;
    private float _inputY;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _at = GetComponent<Animator>();
        faceDirection = 1;
    }

    private bool IsMove()
    {
        return _inputX != 0 || _inputY != 0;
    }

    private void Update()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputY = Input.GetAxisRaw("Vertical");
        _at.SetBool("IsMove", IsMove());

        if (faceDirection == 1 && _inputX < 0)
        {
            faceDirection *= -1;
            transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        }
        if (faceDirection == -1 && _inputX > 0)
        {
            faceDirection *= -1;
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_inputX, _inputY) * _moveSpeed;
    }
}