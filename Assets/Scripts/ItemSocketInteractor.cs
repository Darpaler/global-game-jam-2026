using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ItemSocketInteractor : XRSocketInteractor, IXRSelectFilter
{
    [Header("Item Settings")]
    [SerializeField]
    private ItemType _itemType;

    [SerializeField]
    private bool _consumeItem = false;

    public bool canProcess => enabled; // The filter can process if the script is enabled

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable) 
    {
        Item selectedItem = interactable.transform.GetComponent<Item>();

        if (selectedItem && selectedItem.ItemType == _itemType)
        {
            if (_consumeItem)
                Destroy(interactable.transform.gameObject);

            return true;
        }

        return false;
    }
}
