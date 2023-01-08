using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
     bool placed = false;

    public void SetPlaced(bool value)
    {
        placed = value;
    }

    public bool GetPlaced()
    {
        return placed;
    }
}
