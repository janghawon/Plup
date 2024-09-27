using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using StageDefine;
using UnityEngine.UIElements;
using System;

public class StageEditor : EditorWindow
{
    private static StageData _inEditingData;

    [MenuItem("Window/StageEditor")]
    public static void OpenWindow()
    {
        _inEditingData = default(StageData);
        CreateWindow<StageEditor>();
    }

    private void OnEnable()
    {
        foreach(StageEditorButtonType buttonType in Enum.GetValues(typeof(StageEditorButtonType)))
        {

        }
    }
}
