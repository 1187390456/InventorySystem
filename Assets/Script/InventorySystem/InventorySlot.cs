using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem
{
    [Serializable]
    public class InventorySlot
    {
        public event EventHandler<(ItemStack, bool)> StateChanged; // 定义元组类型事件 或者 定义类生成构造函数事件(多个参数)

        [SerializeField] [Header("物体堆")] private ItemStack _itemStack;
        [SerializeField] [Header("激活状态")] private bool _active;

        // 当前插槽是否有物品
        public bool HasItem => _itemStack?.ItemDefinition != null;

        // 物体定义信息
        public ItemDefinition ItemDefinition => _itemStack?.ItemDefinition; // 一定要判断是否有ItemStack 否则报错

        // 激活状态
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                NotifyItemStackStateChanged();
            }
        }

        // 物体堆
        public ItemStack ItemStack
        {
            get => _itemStack;
            set
            {
                _itemStack = value;
                NotifyItemStackStateChanged();
            }
        }

        // 物体堆数量
        public int Amount
        {
            get => _itemStack.Amount;
            set
            {
                _itemStack.Amount = value;
                NotifyItemStackStateChanged();
            }
        }

        // 通知物体堆状态改变
        private void NotifyItemStackStateChanged()
        {
            StateChanged?.Invoke(this, (_itemStack, _active));
        }

        // 清空该插槽
        public void Clear()
        {
            ItemStack = null;
        }
    }
}