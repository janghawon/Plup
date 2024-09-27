using StageDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PressChangeButton : WindowEdditorButton
{
    protected string[] _texturePathArr;
    protected int _clickCount = 0;

    public override void SetupButton(StageEditorButtonType type, StageData data)
    {
        base.SetupButton(type, data);

        RegisterCallback<ClickEvent>(HandlePressThisButton);
    }

    private void HandlePressThisButton(ClickEvent evt)
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

    protected override void ClickEvent()
    {
        base.ClickEvent();
    }

    protected virtual void ChangeVisual()
    {
        style.backgroundImage =
        Background.FromTexture2D(Resources.Load<Texture2D>(_texturePathArr[_clickCount]));
    }
}
