using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDir
{
    Up,
    Down,
    Left,
    Right
}

public interface IMoveable 
{
    public bool CanMove { get; set; }

    public void MoveToNextTile(MoveDir toMoveDir);
    public void MoveToSpecificTile();
}
