using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Function;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

/*
* Class: UIManager
* Author: ���Ͽ�
* Created: 2024�� 8�� 15�� �����
* Description: UI ���� �� ����
*/

public class UIManager : MonoSingleton<UIManager>
{
    private RectTransform _canvasTrm;
    public RectTransform CanvasTrm
    {
        get
        {
            if(_canvasTrm == null )
            {
                // _canvasTrm�� null�̶�� Canvas�� ã�ƿ´�.
                _canvasTrm = GameObject.FindAnyObjectByType<Canvas>().transform as RectTransform;
            }

            return _canvasTrm;
        }
    }

    // ���� ��Ÿ�� UI���� ����ִ� �迭
    [SerializeField] private SceneUIContent[] _screenElementGroup;

    private Dictionary<SceneType, SceneUIContent> _sceneUIDic = new ();
    private SceneUIContent _currentSceneUIObject;
    public SceneUIContent CurrentSceneUiObject => _currentSceneUIObject;

    private EventSystem _eventSystem;

    private bool _canInteractivle = true;
    public bool CanInteractiveWithUI
    {
        get
        {
            return _canInteractivle;
        }
        set
        {
            // UI�� ��ȣ�ۿ� �� �� ������ eventSystem�� ��Ȱ��ȭ �Ѵ�.
            _eventSystem.enabled = value;
            _canInteractivle = value;
        }
    }

    private void Awake()
    {
        // ���� ������ ���� Dictionary�� ����ش�.
        foreach (SceneUIContent su in _screenElementGroup)
        {
            _sceneUIDic.Add(su.UIType, su);
        }

        // ���� �ε� �Ǳ� �� UI�� �ݱ� ���� ����ó��
        SceneLoader.beforeSceneLoad += HandleSceneUIClose;
        // ���� �ε� �� �� UI�� ��ü�ϱ� ���� ����ó��
        SceneLoader.afterSceneLoad += HandleSceneUIChange;
    }

    private void HandleSceneUIClose(Scene arg0)
    {
        if (_currentSceneUIObject != null)
        {
            // UI �ݱ� �� ���� ó��
            _currentSceneUIObject.SceneUIEnd();
            Destroy(_currentSceneUIObject.gameObject);
        }
    }

    private void HandleSceneUIChange(Scene scene)
    {
        _eventSystem = FindObjectOfType<EventSystem>();
        // �� �̸��� ���� enum���� ��� ��ȯ
        ChangeSceneUIOnChangedScene((SceneType)Enum.Parse(typeof(SceneType), scene.name));
    }

    public void ChangeSceneUIOnChangedScene(SceneType toChangeUIType)
    {
        // ���� ���� ��Ÿ�� UI�� ��ϵ��ִٸ�
        if (_sceneUIDic.ContainsKey(toChangeUIType))
        {
            // UI ����
            SceneUIContent suObject = Instantiate(_sceneUIDic[toChangeUIType], CanvasTrm);
            suObject.gameObject.name = _sceneUIDic[toChangeUIType].gameObject.name + "_[SceneUI]_";

            _currentSceneUIObject = suObject;

            // SceneUIContent ������ UIObject�� ���
            suObject.GenerateOnUIObject();
            // UI ���� �� ó��
            suObject.SceneUIStart();
        }
    }

    // ���� ����ϴ� UIHover�̺�Ʈ
    public void OnScalingProduction(UIObject uiObject, float scaleValue = 1.1f, float scaleTime = 0.2f)
    {
        uiObject.transform.DOScale(scaleValue, scaleTime);
    }

    // ���� ����ϴ� Text�̺�Ʈ
    public void ChangeTextProduction(UIObject label, string text)
    {
        LabelModule labelText = label as LabelModule;
        labelText.SetText(text);
    }
}
