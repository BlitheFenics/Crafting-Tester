using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This lets you get an image since it's a UI elemnt
using UnityEngine.EventSystems; // enables some mouse interactions 

public class ItemSlotScript : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    // This holds what item you have currently in the item slot
    public itemSO currItem;

    // This is the items image
    public Image itemImage;

    // This makes the item able to move around
    public RectTransform itemTransform;

    // This allows you to interact with the item
    private CanvasGroup cg;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        UpdateSlotData();
    }

    public void UpdateSlotData()
    {
        // If theres an item in the slot or placed in the slot this will allow the slot to display the sprite for the item
        if(currItem != null)
        {
            itemImage.enabled = true;
            itemImage.sprite = currItem.itemIcon;
        }
        else // This makes an empty slot display no item sprite
        {
            itemImage.enabled = false;
            itemImage.sprite = null;
        }
        // This makes sure that the item is centered in a slot
        itemTransform.anchoredPosition = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(currItem != null)
        {
            //This lets you drag images around the canvas with some extra math by preventing the canvases scale values from multiplying the images movement value, keeping it 1 - 1 with the mouse
            itemTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
    // makes sure you dont pick up your own item will searching for things in the hover event
    public void OnPointerDown(PointerEventData eventData)
    {
        cg.blocksRaycasts = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bool foundSlot = false;
        // return all of the objects underneath the mouse that are part of the canvas
        foreach(GameObject overObj in eventData.hovered)
        {
            // prevents the object from replacing itself
            if(overObj != gameObject)
            {
                // seeing if the object the mouse is over is an item slot, that way items can only be placed into slots
                if (overObj.GetComponent<ItemSlotScript>())
                {
                    ItemSlotScript itemSlotScript = overObj.GetComponent<ItemSlotScript>();

                    // get the item from the slot and store it
                    itemSO prevItem = currItem;

                    // set the item slot we just cliked on to the item we placed
                    currItem = itemSlotScript.currItem;

                    // set the item in the new slot to the previous items slot, this lets you swap items in the item slots
                    itemSlotScript.currItem = prevItem;

                    // Updates item slots transform so the item is placed into the center of it
                    itemSlotScript.itemTransform.anchoredPosition = Vector3.zero;
                    itemSlotScript.UpdateSlotData();
                    UpdateSlotData();

                    foundSlot = true;
                }
            }
        }

        if(!foundSlot)
        {
            // If a slot isnt found the item will return to its original slot
            itemTransform.anchoredPosition = Vector3.zero;
        }
        cg.blocksRaycasts = true;
    }
}
