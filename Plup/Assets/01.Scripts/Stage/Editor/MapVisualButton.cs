using StageDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;



public class MapVisualButton : PressChangeButton
{
    [Header("MapVisual Block")]
    private VisualElement _mapVisualRoot;
    private const string _mapTileTexturePath = "EditorButtonVisual/StageTile";

    [Header("MapVisual Editor UI")]
    private VisualElement _mapVisualEditRoot;
    private Button _go_left_page_btn;
    private Button _go_right_page_btn;
    private Label _current_page_label;

    private int _blockCountInPage;
    private int _allPageCount;
    private int _currentPageCount = 1;
    private int _countOfBlockInCurrentPage;

    public override void SetupButton(StageEditorButtonType type, VisualElement root, StageData data)
    {
        _texturePathArr = Enum.GetValues(typeof(MapVisualType)).Cast<MapVisualType>()
                                                               .Select(e => e.ToString())
                                                               .ToArray();

        _texturePathArr = _texturePathArr.Select(s => $"EditorButtonVisual/{type}/{s}").ToArray();

        base.SetupButton(type, root, data);

        _mapVisualRoot = new VisualElement();
        _mapVisualRoot.name = "map-visual-root";
        _root.Add(_mapVisualRoot);

        _mapVisualEditRoot = new VisualElement();
        _mapVisualEditRoot.name = "map-visual-edit-root";
        _root.Add(_mapVisualEditRoot);

        _editorWidthChangeEvent += DrawMapVisualEditorUI;
        _editorWidthChangeEvent += GeneratePageShame;
        _editorWidthChangeEvent += ReDrawPage;
    }

    private void DrawMapVisualEditorUI()
    {
        _mapVisualEditRoot.Clear();

        _go_left_page_btn = new Button(HandleGoLeftPage);
        _go_right_page_btn = new Button(HandleGoRightPage);
        _current_page_label = new Label("1 / 1");

        #region Button & Label Generate
        _go_left_page_btn.style.position = Position.Absolute;
        _go_left_page_btn.style.width = StageStandard.mapTile_Page_Go_Btn_Length;
        _go_left_page_btn.style.height = StageStandard.mapTile_Page_Go_Btn_Length;
        _go_left_page_btn.style.backgroundImage =
        Background.FromTexture2D(Resources.Load<Texture2D>("EditorButtonVisual/EditorUI/Left-Arrow-Button"));

        _go_left_page_btn.transform.position =
        new Vector2
        (
            Editor_width - ((StageStandard.mapTile_Page_Go_Btn_Length * 2) + StageStandard.mapTile_Page_Label_Length + (4 * StageStandard.mapTile_Editor_Ui_Interval)),
            (StageStandard.editorBtn_Interval * 2) + StageStandard.editorBtn_Length - StageStandard.mapTile_Page_Go_Btn_Length - StageStandard.mapTile_Editor_Ui_Interval
        );
        _go_right_page_btn.style.position = Position.Absolute;
        _go_right_page_btn.style.width = StageStandard.mapTile_Page_Go_Btn_Length;
        _go_right_page_btn.style.height = StageStandard.mapTile_Page_Go_Btn_Length;
        _go_right_page_btn.style.backgroundImage =
        Background.FromTexture2D(Resources.Load<Texture2D>("EditorButtonVisual/EditorUI/Right-Arrow-Button"));

        _go_right_page_btn.transform.position =
        new Vector2
        (
            Editor_width - (StageStandard.mapTile_Editor_Ui_Interval + (StageStandard.mapTile_Page_Go_Btn_Length)),
            (StageStandard.editorBtn_Interval * 2) + StageStandard.editorBtn_Length - StageStandard.mapTile_Page_Go_Btn_Length - StageStandard.mapTile_Editor_Ui_Interval
        );

        _current_page_label.style.position = Position.Absolute;
        _current_page_label.style.width = StageStandard.mapTile_Page_Label_Length;
        _current_page_label.style.height = StageStandard.mapTile_Page_Go_Btn_Length;
        _current_page_label.style.unityTextAlign = TextAnchor.MiddleCenter;

        _current_page_label.transform.position =
        new Vector2
        (
            Editor_width - (StageStandard.mapTile_Page_Go_Btn_Length + (2 * StageStandard.mapTile_Editor_Ui_Interval) + (StageStandard.mapTile_Page_Label_Length)),
            (StageStandard.editorBtn_Interval * 2) + StageStandard.editorBtn_Length - StageStandard.mapTile_Page_Go_Btn_Length - StageStandard.mapTile_Editor_Ui_Interval
        );
        #endregion

        _mapVisualEditRoot.Add(_go_left_page_btn);
        _mapVisualEditRoot.Add(_go_right_page_btn);
        _mapVisualEditRoot.Add(_current_page_label);
    }

    private void GeneratePageShame()
    {
        int totalBlockCount = _inEditingData.StageBlockCount;
        float blockLength = 3 * (StageStandard.mapTile_Length + StageStandard.mapTile_Interval);

        _blockCountInPage = Mathf.FloorToInt(Editor_width / blockLength);
        _allPageCount = Mathf.Clamp(Mathf.CeilToInt((totalBlockCount * blockLength) / Editor_width), 1, int.MaxValue);

        _current_page_label.text = $"{_currentPageCount} / {_allPageCount}";
    }

    private void HandleGoLeftPage()
    {
        if (_currentPageCount - 1 < 1) return;

        _currentPageCount--;

        GeneratePageShame();
        ReDrawPage();
    }

    private void HandleGoRightPage()
    {
        if (_currentPageCount + 1 > _allPageCount) return;

        _currentPageCount++;

        GeneratePageShame();
        ReDrawPage();
    }

    protected override void HandleClickThisButton()
    {
        MapVisualType blockType = (MapVisualType)_clickCount;
        StageTileElement[,] data = GetMapVisualDataForType(blockType);

        _inEditingData.AddStageBlock(blockType);
        _inEditingData.AddStageTileElement(data);

        GeneratePageShame();

        if (_countOfBlockInCurrentPage >= _blockCountInPage) return;

        ReDrawPage();
    }

    public void ReDrawPage()
    {
        _mapVisualRoot.Clear();

        int totalBlock = (_currentPageCount - 1) * _blockCountInPage;
        int blockCountInCurPage = _inEditingData.StageBlockCount - totalBlock;

        _countOfBlockInCurrentPage = 0;

        for (int i = 0; i < blockCountInCurPage; i++)
        {
            if (_countOfBlockInCurrentPage >= _blockCountInPage) break;

            DrawMapTile(_inEditingData.GetStageTileElementByIndex(totalBlock + i), i);
            _countOfBlockInCurrentPage++;
        }
    }

    public void DrawMapTile(StageTileElement[,] data, int inPangeBlockIndex)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                string path;

                if (data[i, j] == StageTileElement.NormalTile)
                {
                    path = $"{_mapTileTexturePath}/{data[i, j]}_{(j + i) % 2}";
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
                             (inPangeBlockIndex * 3 * (StageStandard.mapTile_Length + StageStandard.mapTile_Interval));

                float yPos = (StageStandard.editorBtn_Interval * 2) + StageStandard.editorBtn_Length + (i * (StageStandard.mapTile_Length + StageStandard.mapTile_Interval));

                tileEle.transform.position = new Vector2(xPos, yPos);
                tileEle.name = $"{_inEditingData.StageBlockCount}-{i}-{j}";
                _mapVisualRoot.Add(tileEle);
            }
        }
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
