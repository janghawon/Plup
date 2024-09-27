using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Function;
using Extension;
using System;
using DG.Tweening;

public class Entity : ExtensionMono, IMoveable, IDieable
{
    public bool CanMove { get; set; }
    public bool IsDie { get; set; }

    [Header("Jump Element")]
    [SerializeField] private float _jumpHeight = 0.2f; // ���� ����
    [SerializeField] private float _movementSpeed = 5f;

    private Vector3 _startPos;     // ���� ��ġ
    private Vector3 _targetPos;    // ��ǥ ��ġ

    private float _jumpTime;       // �����ϴ� �� �ɸ��� ��ü �ð�
    private float _elapsedTime;    // ��� �ð�
    private bool _isJumping;       // ���� ������ ����

    protected virtual void Awake()
    {
    }

    public virtual void MoveToNextTile(MoveDir toMoveDir)
    {
    }

    public virtual void MoveToSpecificTile()
    {
    }

    public void Die()
    {
    }

    protected void JumpToPosition(Vector3 targetPos)
    {
        transform.Translate(0, -transform.position.y, 0);

        _startPos = transform.position;
        _targetPos = targetPos;

        float horizontalDistance = Vector3.Distance(new Vector3(_startPos.x, 0, _startPos.z), new Vector3(targetPos.x, 0, targetPos.z));

        // �����ϴ� �� �ɸ��� �ð� ���
        _jumpTime = horizontalDistance / _movementSpeed;

        // ��� �ð� �ʱ�ȭ
        _elapsedTime = 0f;

        // ���� ����
        _isJumping = true;
    }

    protected virtual void Update()
    {
        if (_isJumping)
        {
            // ��� �ð� ����
            _elapsedTime += Time.deltaTime;

            // ���� ��ġ ���
            Vector3 horizontalPosition = Vector3.Lerp(_startPos, _targetPos, _elapsedTime / _jumpTime);

            // ���� ��ġ ��� (������ ���� ����)
            float verticalPosition = _jumpHeight * 4 * (_elapsedTime / _jumpTime) * (1 - _elapsedTime / _jumpTime);

            // ���� ��ġ ����
            transform.position = new Vector3(horizontalPosition.x, _startPos.y + verticalPosition, horizontalPosition.z);

            Vector3 dir = (_targetPos - _startPos).normalized;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            Quaternion rot = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 20 * Time.deltaTime);

            // ���� �Ϸ� �� ����
            if (_elapsedTime >= _jumpTime)
            {
                _isJumping = false;
                CameraFunction.Shake(VectorFunction.GetRandomVector(Vector3.zero, Vector3.one * 0.02f));

                var dust = PoolManager.Instance.Pop("JumpAndDropParticle") as PoolableParticle;
                dust.transform.position = transform.position;
                dust.Play();
            }
        }
    }
}
