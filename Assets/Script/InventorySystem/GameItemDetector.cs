using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class GameItemDetector : MonoBehaviour
    {
        private Inventory _inventory;

        private void Awake()
        {
            _inventory = transform.parent.GetComponentInChildren<Inventory>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent<GameItem>(out var gameItem) || !_inventory.CanAcceptItemStack(gameItem.ItemStack)) return;
            _inventory.AddItemStack(gameItem.Pick());
        }
    }
}