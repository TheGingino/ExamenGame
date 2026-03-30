using UnityEngine;

public abstract class UsableItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private int quantity;
    
    public int Quantity => quantity;
    
    public virtual void Use()
    {
            if (quantity > 0)
            {
                quantity--;
                Debug.Log($"Used {itemName}. Remaining quantity: {quantity}");
            }
            else
            {
                Debug.Log($"{itemName} is out of stock!");
            }
    }
    
}
