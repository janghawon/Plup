using StageDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;



public class MapVisualButton : PressChangeButton
{
    private VisualElement _mapVisualRoot;
    private const string _mapTileTexturePath = "EditorButtonVisual/StageTile";

    public override void SetupButton(StageEditorButtonType type, VisualElement root, StageData data)
    {
        _texturePathArr = Enum.GetValues(typeof(MapVisualType)).Cast<MapVisualType>()
                                                               .Select(e => e.ToString())
                                                               .ToArray();

        _texturePathArr = _texturePathArr.Select(s => $"EditorButtonVisual/{type}/{s}").ToArray();

        base.SetupButton(type, root, data);

        _mapVisualRoot = new VisualElement();
        _root.Add(_mapVisualRoot);
    }

    protected override void HandleClickThisButton()
    {
        MapVisualType blockType = (MapVisualType)_clickCount;
        StageTileElement[,] data = GetMapVisualDataForType(blockType);

        int blockCnt = _inEditingData.StageBlockCount;
        Debug.Log($"{(blockCnt * (StageStandard.mapTile_Length)) + ((blockCnt + 2) * (StageStandard.mapTile_Interval))}, {editor_width}");
        if ((blockCnt * (StageStandard.mapTile_Length)) + ((blockCnt + 2) * (StageStandard.mapTile_Interval)) > editor_width) return;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                string path;

                if (data[i,j] == StageTileElement.NormalTile)
                {
                    path = $"{_mapTileTexturePath}/{data[i,j]}_{(j + i) % 2}";
                }
                else
                {
                    path = $"{_mapTileTexturePath}/{data[i, j]}";
                }

                Background visual = Background.FromTexture2D(Resources.Load<Texture2D>(path));

                VisualElement tileEle = new VisualElement();

                tileEle.style.position = Position.Absolute;
                tileEle.style.width = StageStandard.mapTile_Length;
                tileEle.style.height = StageStandard.mapTile_Length;

                tileEle.style.backgroundImage = visual;

                float xPos = StageStandard.mapTile_Interval + 
                             j * (StageStandard.mapTile_Length + StageStandard.mapTile_Interval) + 
                             (blockCnt * 3 * (StageStandard.mapTile_Length + StageStandard.mapTile_Interval));

                float yPos = (10 * 2) + 100 + (i * (StageStandard.mapTile_Length + StageStandard.mapTile_Interval));

                tileEle.transform.position = new Vector2(xPos, yPos);

                _mapVisualRoot.Add(tileEle);
            }
        }

        _inEditingData.AddStageBlock(blockType);
        _inEditingData.AddStageTileElement(data);
    }

    private StageTileElement[,] GetMapVisualDataForType(MapVisualType type)
    {
        StageTileElement[,] data;

        switch (type)
        {
            case MapVisualType.Normal:
                data = new StageTileElement[3, 3]
                {
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile},
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile},
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile}
                };
                break;
            case MapVisualType.Bridge:
                data = new StageTileElement[3, 3]
                {
                    {StageTileElement.NormalTile, StageTileElement.Planks, StageTileElement.NormalTile},
                    {StageTileElement.NormalTile, StageTileElement.Water, StageTileElement.NormalTile},
                    {StageTileElement.NormalTile, StageTileElement.Planks, StageTileElement.NormalTile}
                };
                break;
            case MapVisualType.Planks:
                data = new StageTileElement[3, 3]
                {
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile},
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile},
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile}
                };
                break;
            case MapVisualType.Torch:
                data = new StageTileElement[3, 3]
                {
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile},
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile},
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile}
                };
                break;
            case MapVisualType.Well:
                data = new StageTileElement[3, 3]
                {
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile},
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile},
                    {StageTileElement.NormalTile, StageTileElement.NormalTile, StageTileElement.NormalTile}
                };
                break;
            default:
                data = new StageTileElement[3, 3];
                break;
        }

        return data;
    }
}
