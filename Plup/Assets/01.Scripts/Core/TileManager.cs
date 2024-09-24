using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoSingleton<TileManager>
{
    private GameObject _currentStage;
    public int VisualCount 
    {
        get
        {
            if(_currentStage == null)
                _currentStage = GameObject.Find("Stage");
            
            return _currentStage.transform.childCount;
        }
    }
}
