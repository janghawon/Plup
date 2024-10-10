using StageDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageCeator : MonoBehaviour
{
    [Header("StageData Container")]
    [SerializeField] private StageData[] _stageDataContainer;

    [Header("Stage Element")]
    [SerializeField] private GameObject[] _stageBlockContainer;
    private Dictionary<MapVisualType, GameObject> _stageBlockDic = new ();

    [SerializeField] private GameObject[] _tileElementContainer;
    private Dictionary<StageTileElement, GameObject> _tileElementDic = new ();

    private Transform _stageTrm;

    private readonly float _intervalBlockOnBlock = -2.7f;
    private readonly float _tileSize = 0.9f;

    private void Awake()
    {
        _stageTrm = GameObject.Find("Stage").transform;
    }

    private void Start()
    {
        foreach(var block in  _stageBlockContainer)
        {
            _stageBlockDic.Add((MapVisualType)Enum.Parse(typeof(MapVisualType), block.name), block);
        }

        foreach(var tile in _tileElementContainer)
        {
            _tileElementDic.Add((StageTileElement)Enum.Parse(typeof(StageTileElement), tile.name), tile);
        }

        CreateStage(0);
    }

    public void CreateStage(int idx)
    {
        if(idx >= _stageDataContainer.Length)
        {
            Debug.LogError("Error");
            return;
        }

        StageData data = _stageDataContainer[idx];
        Debug.Log(data.StageBlockCount);
        Debug.Log(data.StageTileCount);

        for(int i = 0; i < data.StageBlockCount; i++)
        {
            CreateStagePiece(data.GetMapVisualInfoByIndex(i), i);
            CreateElement(data.GetStageTileElementByIndex(i), i);
        }
    }

    private void CreateStagePiece(MapVisualType type, int idx)
    {
        GameObject block = Instantiate(_stageBlockDic[type], _stageTrm);
        block.transform.position = new Vector3(_intervalBlockOnBlock * idx, 0, 0);
    }

    private void CreateElement(Serializable2DArray<StageTileElement> stageTileElements, int idx)
    {
        for(int i = 0; i < stageTileElements.rows; i++)
        {
            for (int j = 0; i < stageTileElements.cols; j++)
            {
                if (stageTileElements[j, i] == StageTileElement.Obstacle_ON ||
                    stageTileElements[j, i] == StageTileElement.Obstacle_Off ||
                    stageTileElements[j, i] == StageTileElement.Block ||
                    stageTileElements[j, i] == StageTileElement.StartPoint)
                {
                    Vector3 pos = new Vector3(i * _tileSize, 0, (i - 1) * _tileSize) + new Vector3(_intervalBlockOnBlock * idx, 0, 0);
                    var tile = Instantiate(_tileElementDic[stageTileElements[j, i]], _stageTrm);
                    tile.transform.position = pos;

                    
                }
            }
        }
    }
}
