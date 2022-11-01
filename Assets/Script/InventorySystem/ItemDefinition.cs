using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New ItemDefinition Data", menuName = "Inventory Data/ItemDefinition Data")]
    public class ItemDefinition : ScriptableObject
    {
        [SerializeField] [Header("物品名称")] private string _itemName;
        [SerializeField] [Header("能否堆叠")] private bool _canStack;
        [SerializeField] [Header("游戏中显示精灵")] private Sprite _gameSprite;
        [SerializeField] [Header("UI中显示精灵")] private Sprite _uiSprite;

        public string ItemName => _itemName;
        public bool CanStack => _canStack;
        public Sprite GameSprite => _gameSprite;
        public Sprite UiSprite => _uiSprite;

        private void OnValidate()
        {
            if (_gameSprite == null) return;
            _itemName = _gameSprite.name;
        }
    }
}