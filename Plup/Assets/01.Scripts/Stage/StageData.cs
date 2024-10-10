using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StageDefine;

public class StageData : ScriptableObject
{
    private List<MapVisualType> _stageBlockDatabase = new();
    public int StageBlockCount => _stageBlockDatabase.Count;

    private List<Serializable2DArray<StageTileElement>> _stageTileDatabase = new();
    public int StageTileCount => _stageTileDatabase.Count;

    public void AddStageBlock(MapVisualType type)
    {
        _stageBlockDatabase.Add(type);
    }

    public void AddStageTileElement(StageTileElement[,] element)
    {
        if(element.GetLength(0) != 3 || element.GetLength(1) != 3)
        {
            Debug.LogError("Array size is escape in max size");
        }

        Serializable2DArray<StageTileElement> arr = new(element);

        _stageTileDatabase.Add(arr);
    }

    public void RemoveStageBlock(int index = -1)
    {
        if(index == -1) index = _stageBlockDatabase.Count - 1;

        _stageBlockDatabase.RemoveAt(index);
    }

    public void RemoveStageTileElement(int index = -1)
    {
        if (index == -1) index = _stageTileDatabase.Count - 1;

        _stageTileDatabase.RemoveAt(index);
    }

    public MapVisualType GetMapVisualInfoByIndex(int index)
    {
        return _stageBlockDatabase[index];
    }

    public Serializable2DArray<StageTileElement> GetStageTileElementByIndex(int index)
    {
        return _stageTileDatabase[index];
    }

    public void ReplacceStageTileElement(int targetIdx, Serializable2DArray<StageTileElement> element)
    {
        _stageTileDatabase[targetIdx] = element;
    }
}
