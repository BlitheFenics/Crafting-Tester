using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipieManager : MonoBehaviour
{
    // This is a public array of items being set to an array of 3 items
    public ItemSlotScript[] topRow = new ItemSlotScript[3];

    // This is a public array of items being set to an array of 3 items
    public ItemSlotScript[] midRow = new ItemSlotScript[3];

    // This is a public array of items being set to an array of 3 items
    public ItemSlotScript[] bottomRow = new ItemSlotScript[3];

    // this checks all slots at once
    private List<ItemSlotScript[]> allSlots = new List<ItemSlotScript[]>();

    [Space(10)]
    // This is for where the item will end up after the recipe is complete
    public ItemSlotScript outputSlot;

    // This is where all items are stored which you get from the resources folder
    private List<RecipieSO> recipies = new List<RecipieSO>();

    // Start is called before the first frame update
    void Start()
    {
        allSlots.Add(topRow);
        allSlots.Add(midRow);
        allSlots.Add(bottomRow);

        recipies.AddRange(Resources.LoadAll<RecipieSO>("Recipies/"));
    }

    // Update is called once per frame
    void Update()
    {
        foreach(RecipieSO recipie in recipies)
        {
            bool correctPlacement = true;

            // This creates a list of items
            List<itemSO[]> allRecipieSlots = new List<itemSO[]>();
            allRecipieSlots.Add(recipie.topRow);
            allRecipieSlots.Add(recipie.midRow);
            allRecipieSlots.Add(recipie.bottomRow);

            // Goes through each crafting row
            for (int i = 0; i < 3; i++)
            {
                // goes through each crafting slot in the row
                for (int n = 0; n < allRecipieSlots[i].Length; n++)
                {
                    // Makes sure that an row i item slot n isnt empty
                    if (allRecipieSlots[i][n] != null)
                    {
                        // if items in the slots are in the correct place it means you are working towards a valid recipie
                        if (allSlots[i][n].currItem != null)
                        {
                            // if the items in the crafting table dont match a recipie you end up here where its false
                            if (allRecipieSlots[i][n].itemName != allSlots[i][n].currItem.itemName)
                            {
                                correctPlacement = false;
                            }
                        }
                        else // If the item is in a valid recipe slot but the recipe isnt complete, then your recipe placement is false
                        {
                            correctPlacement = false;
                        }
                    }
                    else // the item you placed in the crafting table from the beginning doesnt match a recipe so the placement is false
                    {
                        if(allSlots[i][n].currItem != null)
                        {
                            correctPlacement = false;
                            continue;
                        }
                    }
                }
            }// If your items are in the right spot youll get an item predetemined by the recipe in the output slot
            if (correctPlacement)
            {
                outputSlot.currItem = recipie.output;
                outputSlot.UpdateSlotData();
                break;
            }
            else // if the items placed or lack of items in the crafting table dont resault in an item being made then the output will have nothing
            {
                outputSlot.currItem = null;
                outputSlot.UpdateSlotData();
            }
        }
    }
}
