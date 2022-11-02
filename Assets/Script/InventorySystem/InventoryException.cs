using System;

namespace InventorySystem
{
    public enum InventoryOperation
    {
        Add,
        Remove
    }

    // 库存系统异常捕获
    public class InventoryException : Exception
    {
        public InventoryOperation operation { get; }

        public InventoryException(string message, InventoryOperation operation) : base($"{operation} Error:{message}")
        {
            this.operation = operation;
        }
    }
}