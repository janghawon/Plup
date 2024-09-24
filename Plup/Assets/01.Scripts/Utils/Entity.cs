using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Function;
using Extension;

public class Entity : ExtensionMono, IMoveable, IDieable
{
    public bool CanMove { get; set; }
    public bool IsDie { get; set; }

    public void MoveToNextTile()
    {
    }

    public void MoveToSpecificTile()
    {
    }

    public void Die()
    {
    }
}
