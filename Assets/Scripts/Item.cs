using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM
{
    Coin,
    Life,
    Point,
}

public class Item : MonoBehaviour
{
    [SerializeField] ITEM type;

    public ITEM Type => type;       // �б� ���� ������Ƽ.
}
