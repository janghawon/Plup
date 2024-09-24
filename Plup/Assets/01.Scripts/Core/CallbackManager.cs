using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackManager : MonoSingleton<CallbackManager>
{
    // �ݹ��� ���� Dictionaary
    private Dictionary<string, Action<object[]>> _actionCallbackDic = new ();

    // ����ִ� �ݹ� �ʱ�ȭ
    public void ClearAllCallback()
    {
        _actionCallbackDic.Clear();
    }

    // �ݹ� ���
    public void RegisterCallback(Action<object[]> callback, string eventkey)
    {
        if(_actionCallbackDic.ContainsKey(eventkey)) 
        {
            // eventKey�� �̹� �ִٸ� �̹� �����ϴ� �ݹ鿡 ���� ó��
            _actionCallbackDic[eventkey] += callback;
            return;
        }

        // eventKey�� ���ٸ� ���Ӱ� �߰� 
        _actionCallbackDic.Add(eventkey, callback);
    }

    public void UnRegisterCallback(Action<object[]> callback, string eventkey)
    {
        if (_actionCallbackDic.ContainsKey(eventkey))
        {
            // eventKey�� �ִٸ� �̹� �����ϴ� �ݹ鿡 ���� ���� ó��
            _actionCallbackDic[eventkey] -= callback;
        }
        else
        {
            Debug.LogError($"Error : Not Exist Callback : {eventkey}");
        }
    }

    // eventKey�� �ش��ϴ� �ݹ� ��� ����
    public void RemoveCallback(string eventkey) 
    {
        _actionCallbackDic[eventkey] = null;
        _actionCallbackDic.Remove(eventkey);
    }

    // �ݹ� ����
    // params object[]�� ���� ���ϰ� �ް� ������ �ѱ� �� �ִ�.
    public void Callback(string eventkey, params object[] arr) 
    {
        _actionCallbackDic[eventkey]?.Invoke(arr);
    }
}
