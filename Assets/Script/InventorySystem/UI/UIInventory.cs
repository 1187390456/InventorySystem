using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField] [Header("目标库存")] private Inventory _inventory;
        [SerializeField] [Header("UI插槽预制件")] private GameObject _uiSlotPrefabs;
        [SerializeField] [Header("UI插槽列表")] private List<UIInventorySlot> _slots;


        public Inventory Inventory => _inventory;

        [ContextMenu("Init Inventory UI")]
        private void InitInventoryUI()
        {
            if (_inventory == null || _uiSlotPrefabs == null) return;
            _slots = new List<UIInventorySlot>(_inventory.Size);
            for (var i = 0; i < _inventory.Size; i++)
            {
                var uiSlot = Instantiate(_uiSlotPrefabs, transform);
                var uiScript = uiSlot.GetComponent<UIInventorySlot>();
                uiScript.RenderUISlot(i);
                _slots.Add(uiScript);
            }
        }
    }
}