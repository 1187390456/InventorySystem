using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class UIInventorySlot : MonoBehaviour
    {
        [SerializeField] [Header("当前库存")] private Inventory _inventory;
        [SerializeField] [Header("当前插槽")] private InventorySlot _slot;
        [SerializeField] [Header("当前插槽索引")] private int SlotIndex;

        [SerializeField] [Header("显示图标")] private Image _itemIcon;
        [SerializeField] [Header("激活指示器")] private Image _activeIndicator;
        [SerializeField] [Header("数量")] private TMP_Text _amount;

        private void Start()
        {
            RenderUISlot(SlotIndex); // 订阅事件
        }

        // 渲染Slot UI
        public void RenderUISlot(int index)
        {
            if (_slot != null) _slot.StateChanged -= OnStateChange;
            SlotIndex = index;
            if (_inventory == null) _inventory = GetComponentInParent<UIInventory>().Inventory; // 获取目标库存
            _slot = _inventory.Slots[SlotIndex]; //获取库存当前索引插槽
            _slot.StateChanged += OnStateChange;
            UpdateViewState(_slot.ItemStack, _slot.Active);
        }

        // 更新方法
        private void UpdateViewState(ItemStack itemStack, bool active)
        {
            var item = itemStack?.ItemDefinition;
            var hasItem = item != null;
            var canStack = hasItem && item.CanStack;

            _activeIndicator.enabled = active;
            _itemIcon.enabled = hasItem;
            _amount.enabled = canStack;

            if (!hasItem) return;

            _itemIcon.sprite = item.UiSprite;

            if (canStack && itemStack.Amount > 0) _amount.SetText(itemStack.Amount.ToString());
        }

        // 事件订阅
        private void OnStateChange(object sender, (ItemStack itemStack, bool isActive) e)
        {
            UpdateViewState(e.itemStack, e.isActive);
        }
    }
}