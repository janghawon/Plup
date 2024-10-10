using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using StageDefine;
using UnityEngine.UIElements;
using System;
using UnityEditor.Callbacks;

public class StageEditor : EditorWindow
{
    private float _lastWindow_Length;

    private static StageData _inEditingData;
    private List<WindowEditorButton> _stageEditorButtonList = new();
    private TextField _stageNameInput;
    private Button _stageDataSaveBtn;

    public Action<MouseDownEvent> editorClickEvent;

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

    private void OnEnable()
    {
        _lastWindow_Length = position.size.x;
        InitializeEditorButton();
        InitializeEditorTextField();
        InitializeDataSaveButton();

        rootVisualElement.RegisterCallback<MouseDownEvent>(CallToClickEvent);
    }

    private void OnDisable()
    {
        rootVisualElement.UnregisterCallback<MouseDownEvent>(CallToClickEvent);
    }

    private void CallToClickEvent(MouseDownEvent evt)
    {
        if(evt.button == 0)
        {
            editorClickEvent?.Invoke(evt);
        }
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
                button.Editor_width = _currentWindow_Length;
            }

            ReGenerateTextFieldPosition();
            ReGenerateDataSaveButtonPosition();
        }
    }

    #region EditorSaveButton
    /// <summary>
    /// 데이터 저장 버튼 생성 및 위치 정형화 코드
    /// </summary>
    private void InitializeDataSaveButton()
    {
        _stageDataSaveBtn = new Button(HandleSaveData);
        _stageDataSaveBtn.text = "저장";
        _stageDataSaveBtn.style.width = StageStandard.stageData_SaveBtn_Width;
        _stageDataSaveBtn.style.height = StageStandard.stageData_SaveBtn_Height;

        rootVisualElement.Add(_stageDataSaveBtn);
    }

    private void HandleSaveData()
    {
        string path = AssetDatabase.GetAssetPath(_inEditingData);
        AssetDatabase.RenameAsset(path, _stageNameInput.text);
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(_inEditingData);
    }

    private void ReGenerateDataSaveButtonPosition()
    {
        _stageDataSaveBtn.transform.position =
        new Vector2(position.size.x - StageStandard.stageData_SaveBtn_Width - StageStandard.stageData_SaveBtn_Interval,
                    StageStandard.stageData_TextField_Interval + StageStandard.stageData_SaveBtn_Interval + StageStandard.stageData_TextField_Height);
    }
    #endregion

    #region EditorTextField
    /// <summary>
    /// 에디터 텍스트 필드 생성 및 위치 정형화 코드
    /// 
    private void InitializeEditorTextField()
    {
        _stageNameInput = new TextField("스테이지 이름");
        _stageNameInput.style.width = StageStandard.stageData_TextField_Width;
        _stageNameInput.style.height = StageStandard.stageData_TextField_Height;
        _stageNameInput.style.position = Position.Absolute;
        _stageNameInput.labelElement.style.minWidth = 10;

        rootVisualElement.Add(_stageNameInput);
    }
    private void ReGenerateTextFieldPosition()
    {
        _stageNameInput.transform.position = 
        new Vector2(position.size.x - StageStandard.stageData_TextField_Width - StageStandard.stageData_TextField_Interval, StageStandard.stageData_TextField_Interval);
    }
    #endregion

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
            button.editor = this;
            button.Editor_width = _lastWindow_Length;
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
