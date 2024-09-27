using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private PlayerInputReader _inputReader;

    private Vector3 _startPosition;
    private const float _tileLength = 0.9f;
    private float _maxTileXValue;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _maxTileXValue = TileManager.Instance.VisualCount * -_tileLength * 3;
        CallbackManager.Instance.RegisterCallback(PlayerMovement, "PlayerMove");
        _startPosition = transform.position;
    }

    private void PlayerMovement(object[] arr)
    {
        MoveToNextTile((MoveDir)arr[0]);
    }

    private float GetFitTileLength(float value)
    {
        return Mathf.Round(value / _tileLength) * _tileLength;
    }

    public override void MoveToNextTile(MoveDir toMoveDir)
    {
        base.MoveToNextTile(toMoveDir);

        Vector3 toMovePosition = new Vector3(GetFitTileLength(_startPosition.x), 0, GetFitTileLength(_startPosition.z));

        switch (toMoveDir)
        {
            case MoveDir.Up:
                toMovePosition += new Vector3(0, 0, -_tileLength);
                break;
            case MoveDir.Down:
                toMovePosition += new Vector3(0, 0, _tileLength);
                break;
            case MoveDir.Left:
                toMovePosition += new Vector3(_tileLength, 0, 0);
                break;
            case MoveDir.Right:
                toMovePosition += new Vector3(-_tileLength, 0, 0);
                break;
        }

        if (toMovePosition.x < _maxTileXValue || toMovePosition.x > 0 || toMovePosition.z < -0.9f || toMovePosition.z > 0.9f) return;

        _startPosition = toMovePosition;
        JumpToPosition(toMovePosition);
    }

    public override void MoveToSpecificTile()
    {
        base.MoveToSpecificTile();
    }

    protected override void Update()
    {
        base.Update();
    }
}
