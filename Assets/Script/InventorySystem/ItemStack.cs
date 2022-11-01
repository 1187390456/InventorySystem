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
        [SerializeField] [Header("物品数量")] private int _amount;

        public ItemDefinition ItemDefinition => _itemDefinition;
        public bool CanStack => _itemDefinition && _itemDefinition.CanStack;

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