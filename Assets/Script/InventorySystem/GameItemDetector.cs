using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<GameItem>(out var gameItem)) return;
        gameItem.Pick();
    }
}