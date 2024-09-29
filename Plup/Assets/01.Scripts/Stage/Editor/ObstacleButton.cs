using StageDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleButton : PressChangeButton
{
    public override void SetupButton(StageEditorButtonType type, VisualElement root, StageData data)
    {
        _texturePathArr = Enum.GetValues(typeof(ObstacleType)).Cast<ObstacleType>()
                                                               .Select(e => e.ToString())
                                                               .ToArray();

        _texturePathArr = _texturePathArr.Select(s => $"EditorButtonVisual/{type}/{s}").ToArray();

        base.SetupButton(type, root, data);
    }

    protected override void HandleClickThisButton()
    {
        
    }
}
