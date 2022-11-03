using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class UIInventorySlot : MonoBehaviour
    {
        private Inventory _inventory;
        private InventorySlot _slot;

        [SerializeField]  private Image _itemIcon;
        [SerializeField] private Image _activeIndicator;
        [SerializeField] private TMP_Text _amount;

        private void Start()
        {
            RenderUISlot(1);
        }


        // 渲染Slot UI
        public void RenderUISlot(int index)
        {
            _inventory = GetComponentInParent<UIInventory>().Inventory; // 获取目标库存
            _slot = _inventory.Slots[index]; //获取库存当前索引插槽
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

            if (canStack && itemStack.Amount>0) _amount.SetText(itemStack.Amount.ToString());

        }
        // 事件订阅
        private void OnStateChange(object sender,(ItemStack itemStack, bool isActive)e)
        {
            UpdateViewState(e.itemStack, e.isActive);
        }

    }
}