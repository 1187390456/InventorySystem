using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class GameItem : MonoBehaviour
    {
        [SerializeField] [Header("当前精灵渲染")] private SpriteRenderer _spriteRenderer;
        [SerializeField] [Header("物体堆")] private ItemStack _itemStack;

        [SerializeField] private float _colliderEnableAfter = 1.0f;
        [SerializeField] private float _throwGravity = 2.0f;
        [SerializeField] private float _maxForce = 5.0f;
        [SerializeField] private float _minForce = 3.0f;
        [SerializeField] private float _throwYForce = 5.0f;

        private Rigidbody2D _rb;
        private Collider2D _collider;

        public ItemStack ItemStack
        {
            get => _itemStack;
            set => _itemStack = value;
        }

        public int Amount => _itemStack.Amount;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _collider.enabled = false;
        }

        private void Start()
        {
            SetUpGameItem(); // 同步自动化
            StartCoroutine(OpenCollider(_colliderEnableAfter)); // 协程开启碰撞
        }

        private void OnValidate()
        {
            SetUpGameItem();
        }

        // 协程开启碰撞 1秒后执行 防止扔出后立马捡回来
        private IEnumerator OpenCollider(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _collider.enabled = true;
        }

        // 协程关闭重力 小于指定重力执行
        private IEnumerator CloseGravity(float yVelocity)
        {
            yield return new WaitUntil(() => _rb.velocity.y < -yVelocity);
            _rb.velocity = Vector2.zero; // 速度归0
            _rb.gravityScale = 0; // 重力大小归0
        }

        // 自动化
        private void SetUpGameItem()
        {
            if (_itemStack == null) return;

            _spriteRenderer.sprite = _itemStack.ItemDefinition.GameSprite;
            _itemStack.Amount = _itemStack.Amount;
            var name = _itemStack.ItemDefinition.ItemName;
            var number = _itemStack.CanStack ? Amount.ToString() : "not allow stack";
            gameObject.name = $"{name}({number})";
        }

        // 被拾取
        public ItemStack Pick()
        {
            Destroy(gameObject);
            return _itemStack;
        }

        // 扔出去
        public void Throw(int xDir)
        {
            _rb.gravityScale = _throwGravity;
            var randXForce = Random.Range(_minForce, _maxForce);
            _rb.velocity = new Vector2(randXForce * Mathf.Sign(xDir), _throwYForce);
            StartCoroutine(CloseGravity(_throwYForce));
        }
    }
}