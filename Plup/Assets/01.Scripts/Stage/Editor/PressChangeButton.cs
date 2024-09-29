using StageDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PressChangeButton : WindowEditorButton
{
    protected string[] _texturePathArr;
    protected int _clickCount = 0;

    public override void SetupButton(StageEditorButtonType type, VisualElement root, StageData data)
    {
        base.SetupButton(type, root, data);
        RegisterCallback<PointerDownEvent>(HandlePressThisButton);
    }

    private void HandlePressThisButton(PointerDownEvent evt)
    {
        if(evt.button == 1)
        {
            _clickCount++;

            if(_clickCount == _texturePathArr.Length)
            {
                _clickCount = 0;
            }

            ChangeVisual();
        }
    }

    private void ChangeVisual()
    { 
        style.backgroundImage =
        Background.FromTexture2D(Resources.Load<Texture2D>(_texturePathArr[_clickCount]));
    }
}
