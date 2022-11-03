using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb;
    private Animator _at;
    private Inventory _inventory;

    private int faceDirection = 1;

    private float _inputX;
    private float _inputY;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _at = GetComponent<Animator>();
        _inventory = GetComponentInChildren<Inventory>();
    }

    private void Start()
    {
        _inventory.SetActiveIndex(0);
    }

    private bool IsMove() => _inputX != 0 || _inputY != 0;

    private void Update()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputY = Input.GetAxisRaw("Vertical");
        _at.SetBool("IsMove", IsMove());

        if (faceDirection == 1 && _inputX < 0)
        {
            Turn();
        }
        if (faceDirection == -1 && _inputX > 0)
        {
            Turn();
        }
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _inventory.SetActiveIndex(_inventory.ActiveSlotIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _inventory.SetActiveIndex(_inventory.ActiveSlotIndex - 1);
        }
    }

    private void Turn()
    {
        faceDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_inputX, _inputY) * _moveSpeed;
    }
}