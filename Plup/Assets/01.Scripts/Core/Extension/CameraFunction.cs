using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraFunction
{
    private static CinemachineVirtualCamera _vcam;
    public static CinemachineVirtualCamera VirtualCamera
    {
        get
        {
            if(_vcam == null )
                _vcam = GameObject.FindFirstObjectByType<CinemachineVirtualCamera>();

            return _vcam;
        }
    }

    private static CinemachineImpulseSource _source;

    public static void Shake(Vector3 value)
    {
        if(_source == null)
            _source = VirtualCamera.GetComponent<CinemachineImpulseSource>();

        _source.m_DefaultVelocity = value;
        _source.GenerateImpulse();
    }
}
