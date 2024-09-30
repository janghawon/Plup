using StageDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class WindowEditorButton : Button
{
    protected event Action _editorWidthChangeEvent;

    private float _editor_width;
    public float Editor_width
    {
        get
        {
            return _editor_width;
        }
        set
        {
            _editor_width = value;
            _editorWidthChangeEvent?.Invoke();
        }
    }
    public StageEditor editor;
    protected VisualElement _root;
    protected StageData _inEditingData;

    public virtual void SetupButton(StageEditorButtonType type, VisualElement root, StageData data)
    {
        style.backgroundImage =
        Background.FromTexture2D(Resources.Load<Texture2D>($"EditorButtonVisual/{type}"));

        _root = root;
        _inEditingData = data;

        RegisterCallback<ClickEvent>(HandleClickCheck);
    }

    private void HandleClickCheck(ClickEvent e)
    {
        if(e.button == 0)
        {
            editor.editorClickEvent = null;
            HandleClickThisButton(e);

            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(_inEditingData);
        }
    }

    protected abstract void HandleClickThisButton(ClickEvent evt);
}
