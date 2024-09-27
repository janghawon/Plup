using StageDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class WindowEdditorButton : Button
{
    public Action ClickEvent = null;

    public virtual void SetupButton(StageEditorButtonType type)
    {
        style.backgroundImage =
        Background.FromTexture2D(Resources.Load<Texture2D>($"EditorButtonVisual/{type}"));

        RegisterCallback<ClickEvent>(HandleCreateMap);
    }

    private void HandleCreateMap(ClickEvent evt)
    {
        if(evt.button == 0)
        {
            ClickEvent?.Invoke();
        }
    }
}
