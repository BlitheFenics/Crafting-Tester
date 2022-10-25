using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This lets us make a scriptable object with four variables inside the editor
[CreateAssetMenu(fileName = "New recipie", menuName = "Item Manager/New Recipie")]
public class RecipieSO : ScriptableObject
{
    // This is a public array of items being set to an array of 3 items
    public itemSO[] topRow = new itemSO[3];

    // This is a public array of items being set to an array of 3 items
    public itemSO[] midRow = new itemSO[3];

    // This is a public array of items being set to an array of 3 items
    public itemSO[] bottomRow = new itemSO[3];

    public itemSO output;
}
