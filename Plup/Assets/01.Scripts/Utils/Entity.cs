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
    [SerializeField] private float _jumpHeight = 0.2f; // 점프 높이
    [SerializeField] private float _movementSpeed = 5f;

    private Vector3 _startPos;     // 시작 위치
    private Vector3 _targetPos;    // 목표 위치

    private float _jumpTime;       // 점프하는 데 걸리는 전체 시간
    private float _elapsedTime;    // 경과 시간
    private bool _isJumping;       // 점프 중인지 여부

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

        // 점프하는 데 걸리는 시간 계산
        _jumpTime = horizontalDistance / _movementSpeed;

        // 경과 시간 초기화
        _elapsedTime = 0f;

        // 점프 시작
        _isJumping = true;
    }

    protected virtual void Update()
    {
        if (_isJumping)
        {
            // 경과 시간 증가
            _elapsedTime += Time.deltaTime;

            // 수평 위치 계산
            Vector3 horizontalPosition = Vector3.Lerp(_startPos, _targetPos, _elapsedTime / _jumpTime);

            // 수직 위치 계산 (포물선 공식 적용)
            float verticalPosition = _jumpHeight * 4 * (_elapsedTime / _jumpTime) * (1 - _elapsedTime / _jumpTime);

            // 최종 위치 적용
            transform.position = new Vector3(horizontalPosition.x, _startPos.y + verticalPosition, horizontalPosition.z);

            Vector3 dir = (_targetPos - _startPos).normalized;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            Quaternion rot = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 20 * Time.deltaTime);

            // 점프 완료 시 멈춤
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
