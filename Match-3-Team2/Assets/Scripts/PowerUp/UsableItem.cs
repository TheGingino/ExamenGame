using UnityEngine;

public abstract class UsableItem : MonoBehaviour
{
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _itemIcon;
    [SerializeField] private int _quantity;
    
    public int Quantity => _quantity;
    
    public virtual void Use()
    {
        if (_quantity > 0) 
        {
            _quantity--; 
            Debug.Log($"Used {_itemName}. Remaining quantity: {_quantity}"); 
        }
        else 
        { 
            Debug.Log($"{_itemName} is out of stock!");
        }
    }
}
