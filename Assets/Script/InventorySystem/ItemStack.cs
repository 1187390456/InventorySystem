using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class ItemStack
    {
        [SerializeField] [Header("物体定义信息")] private ItemDefinition _itemDefinition;
        [SerializeField] [Header("物品数量")] private int _amount = 1;

        public ItemStack(ItemDefinition itemDefinition, int amount)
        {
            _itemDefinition = itemDefinition;
            _amount = amount;
        }

        public ItemStack()
        { }

        public ItemDefinition ItemDefinition => _itemDefinition;
        public bool CanStack => _itemDefinition && _itemDefinition.CanStack;

        // 数量
        public int Amount
        {
            get => _amount;
            set
            {
                value = Mathf.Clamp(value, 1, 999);
                _amount = CanStack ? value : 1;
            }
        }
    }
}