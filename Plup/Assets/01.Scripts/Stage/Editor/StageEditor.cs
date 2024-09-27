using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using StageDefine;
using UnityEngine.UIElements;
using System;

public class StageEditor : EditorWindow
{
    [Header("SettingValue")]
    private float _editorBtn_Length = 100.0f;
    private float _editorBtn_Interval = 10.0f;

    private static StageData _inEditingData;
    private List<WindowEdditorButton> _stageEditorButtonList = new();

    [MenuItem("Window/StageEditor")]
    public static void OpenWindow()
    {
        _inEditingData = default(StageData);
        CreateWindow<StageEditor>();
    }

    private void OnEnable()
    {
        Dictionary<StageEditorButtonType, Action> clickEventDic = new();
        clickEventDic.Add(StageEditorButtonType.MapVisual, CreateMapVisual);

        InitializeEditorButton(clickEventDic);
    }

    private void OnGUI()
    {
        GenerateEditorButton();
    }

    #region EditorButton
    /// <summary>
    /// 에디터 버튼 생성 및 위치 정형화 코드 
    /// </summary>
    private void InitializeEditorButton(Dictionary<StageEditorButtonType, Action> clickEventDic)
    {
        foreach (StageEditorButtonType buttonType in Enum.GetValues(typeof(StageEditorButtonType)))
        {
            Type type = Type.GetType($"{buttonType}Button");

            if (type == null)
            {
                Debug.LogError($"Error : Not has exist button Type = {buttonType}");
                continue;
            }

            var button = Activator.CreateInstance(type) as WindowEdditorButton;
            button.SetupButton(buttonType);
            button.ClickEvent += () => CallbackManager.Instance.Callback(buttonType.ToString());

            _stageEditorButtonList.Add(button);

            rootVisualElement.Add(button);
        }
    }
    private void GenerateEditorButton()
    {
        for (int i = 0; i < _stageEditorButtonList.Count; i++)
        {
            WindowEdditorButton button = _stageEditorButtonList[i];

            button.style.position = Position.Absolute;
            button.style.width = _editorBtn_Length;
            button.style.height = _editorBtn_Length;

            float xPos = _editorBtn_Interval + i * (_editorBtn_Length + _editorBtn_Interval);
            float yPos = _editorBtn_Interval;

            button.transform.position = new Vector2(xPos, yPos);
        }
    }
    #endregion

    #region MapVisual
    private void CreateMapVisual()
    {

    }
    #endregion
}
