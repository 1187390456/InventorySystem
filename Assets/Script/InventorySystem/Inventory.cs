using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] [Header("库存大小")] private int _size;
        [SerializeField] [Header("插槽列表")] private List<InventorySlot> _slots = new List<InventorySlot>();

        private void OnValidate()
        {
            SetupSlotSize();
        }

        // 根据库存大小 自动化插槽列表
        private void SetupSlotSize()
        {
            if (_size < 0) _size = 0;
            if (_size > _slots.Count) _slots.AddRange(new InventorySlot[_size - _slots.Count]);
            if (_size < _slots.Count) _slots.RemoveRange(_size, _slots.Count - _size);
        }

        // 接收物体条件 库存未满 或者 满了但是有可堆叠物品存在插槽中

        public bool CanAcceptItemStack(ItemStack itemStack) => !IsFull() || FindSlotByItemStack(itemStack.ItemDefinition, true) != null;

        //添加物品
        public ItemStack AddItemStack(ItemStack itemStack)
        {
            var slot = FindSlotByItemStack(itemStack.ItemDefinition, true);
            // 库存满了 且 找不到能堆叠的插槽
            if (IsFull() && slot == null) throw new InventoryException("Inventory is Full !", InventoryOperation.Add);
            // 找到可堆叠的插槽
            if (slot != null) slot.Amount += itemStack.Amount;
            // 未满 但是未找到可堆叠插槽 即找到一个空的存进去
            else
            {
                var emptySlot = _slots.First(x => !x.HasItem);
                emptySlot.ItemStack = itemStack;
            }
            return itemStack;
        }

        // 判断是否库存已满
        public bool IsFull() => _slots.Count(x => x.HasItem) >= _size;

        // 通过物品信息 寻找存放物体的插槽 可选参数 是否只是 可堆叠物品
        public InventorySlot FindSlotByItemStack(ItemDefinition itemDefinition, bool onlyCanStack = false)
            => _slots.FirstOrDefault(x => x.ItemDefinition == itemDefinition && (itemDefinition.CanStack || !onlyCanStack));
    }
}