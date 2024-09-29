using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StageDefine;

public class StageData : ScriptableObject
{
    private List<MapVisualType> _stageBlockDatabase = new();
    public int StageBlockCount => _stageBlockDatabase.Count;

    private List<StageTileElement[,]> _stageTileDatabase = new();

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

        _stageTileDatabase.Add(element);
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

    public StageTileElement[,] GetLastStageBlock()
    {
        return _stageTileDatabase[_stageTileDatabase.Count - 1];
    }

    public StageTileElement[,] GetStageBlockByIndex(int index)
    {
        return _stageTileDatabase[index];
    }
}
