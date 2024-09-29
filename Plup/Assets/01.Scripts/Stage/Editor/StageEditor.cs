using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using StageDefine;
using UnityEngine.UIElements;
using System;

public class StageEditor : EditorWindow
{
    private float _lastWindow_Length;

    private static StageData _inEditingData;
    private List<WindowEditorButton> _stageEditorButtonList = new();

    [MenuItem("Window/StageEditor")]
    public static void OpenWindow()
    {
        _inEditingData = ScriptableObject.CreateInstance<StageData>();
        AssetDatabase.CreateAsset(_inEditingData, $"Assets/03.SO/StageData/UnknownStage.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(_inEditingData);

        CreateWindow<StageEditor>();
    }

    private static void OpenWindow(StageData data)
    {
        _inEditingData = data;
        CreateWindow<StageEditor>();
    }

    public static bool OpenAtSo(int instanceID, int line)
    {
        StageData data = EditorUtility.InstanceIDToObject(instanceID) as StageData;

        if (data != null)
        {
            OpenWindow(data);
            return true;
        }

        return false;
    }

    private void OnEnable()
    {
        InitializeEditorButton();

        _lastWindow_Length = position.size.x;
    }

    private void OnGUI()
    {
        GenerateEditorButton();

        float _currentWindow_Length = position.size.x;

        if(_lastWindow_Length != _currentWindow_Length)
        {
            _lastWindow_Length = _currentWindow_Length;

            foreach (var button in _stageEditorButtonList)
            {
                button.editor_width = _currentWindow_Length;
            }
        }
    }

    #region EditorButton
    /// <summary>
    /// 에디터 버튼 생성 및 위치 정형화 코드 
    /// </summary>
    private void InitializeEditorButton()
    {
        foreach (StageEditorButtonType buttonType in Enum.GetValues(typeof(StageEditorButtonType)))
        {
            Type type = Type.GetType($"{buttonType}Button");

            if (type == null)
            {
                Debug.LogError($"Error : Not has exist button Type = {buttonType}");
                continue;
            }

            var button = Activator.CreateInstance(type) as WindowEditorButton;
            button.SetupButton(buttonType, rootVisualElement, _inEditingData);

            _stageEditorButtonList.Add(button);
            rootVisualElement.Add(button);
        }
    }
    private void GenerateEditorButton()
    {
        for (int i = 0; i < _stageEditorButtonList.Count; i++)
        {
            WindowEditorButton button = _stageEditorButtonList[i];

            button.style.position = Position.Absolute;
            button.style.width = StageStandard.editorBtn_Length;
            button.style.height = StageStandard.editorBtn_Length;

            float xPos = StageStandard.editorBtn_Interval + i * (StageStandard.editorBtn_Length + StageStandard.editorBtn_Interval);
            float yPos = StageStandard.editorBtn_Interval;

            button.transform.position = new Vector2(xPos, yPos);
        }
    }
    #endregion
}
