using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable 
{
    public bool CanMove { get; set; }

    public void MoveToNextTile();
    public void MoveToSpecificTile();
}
