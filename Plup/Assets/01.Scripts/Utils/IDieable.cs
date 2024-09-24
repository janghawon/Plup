using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDieable 
{
    public bool IsDie { get; set; }

    public void Die();
}
