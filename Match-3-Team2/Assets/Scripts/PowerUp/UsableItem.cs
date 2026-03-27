using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private int quantity;
    
    [SerializeField] private Transform originalPosition;

    public string ItemName => itemName;
    public Sprite ItemIcon => itemIcon;
    public int Quantity => quantity;
    
    public virtual void Use()
    {
        if (quantity <= 0) return;
        quantity--;
    }

    public virtual void OnDrag()
    {
        if (!Input.GetMouseButton(0)) return;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
    }
}
