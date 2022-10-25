using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This lets us make a scriptable object with two variables inside the editor
[CreateAssetMenu(fileName ="New item", menuName ="Item Manager/New Item")]
public class itemSO : ScriptableObject
{
    // This holds the image for the object
    public Sprite itemIcon;

    // This holds the name for the object
    public string itemName;
}
