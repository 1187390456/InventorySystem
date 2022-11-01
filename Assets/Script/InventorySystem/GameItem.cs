using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class GameItem : MonoBehaviour
    {
        [SerializeField] [Header("当前精灵渲染")] private SpriteRenderer _spriteRenderer;
        [SerializeField] [Header("物体堆")] private ItemStack _itemStack;
        public ItemStack ItemStack => _itemStack;
        public int Amount => _itemStack.Amount;

        private void OnValidate()
        {
            SetUpGameItem();
        }

        private void SetUpGameItem()
        {
            if (_itemStack == null) return;
            _spriteRenderer.sprite = _itemStack.ItemDefinition.GameSprite;
            _itemStack.Amount = _itemStack.Amount; // 这里重新赋值刷新限制
            var name = _itemStack.ItemDefinition.ItemName;
            var number = _itemStack.CanStack ? Amount.ToString() : "not allow stack";
            gameObject.name = $"{name}({number})";
        }
    }
}