using StageDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class WindowEditorButton : Button
{
    public float editor_width;
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
            HandleClickThisButton();
        }
    }

    protected abstract void HandleClickThisButton();
}
