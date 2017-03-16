using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : InventoryItem
{
    public enum Type
    {
        Brass,
        Silver,
        Gold,
    }

    public Type CoinType;
}
