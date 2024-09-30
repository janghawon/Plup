using StageDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class StartPointButton : WindowEditorButton
{
    protected override void HandleClickThisButton(ClickEvent evt)
    {
        editor.editorClickEvent += HandleChangeTileElement;
    }

    private void HandleChangeTileElement(MouseDownEvent evt)
    {
        Vector2 mousePos = evt.mousePosition;
        VisualElement clickEle = _root.panel.Pick(mousePos);

        string[] arr = clickEle.name.Split('-');
        if (arr.Length != 3) return;

        int[] nums = arr.Select(i => Convert.ToInt32(i)).ToArray();

        StageTileElement[,] newTile = _inEditingData.GetStageTileElementByIndex(nums[0]);
        StageTileElement toChangeTile = StageTileElement.StartPoint;
        newTile[nums[1], nums[2]] = toChangeTile;
        _inEditingData.ReplacceStageTileElement(nums[0], newTile);

        string path = $"EditorButtonVisual/StageTile/{toChangeTile}";
        clickEle.style.backgroundImage = Background.FromTexture2D(Resources.Load<Texture2D>(path));
    }
}
