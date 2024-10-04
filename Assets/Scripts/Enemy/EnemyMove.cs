using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyMove : IEnemyState
{
    EnemyBase _enemy;
    private Rigidbody2D _rigidbody2D;
    private Quaternion _targetRotation; //��ǥ ����
    private Vector3 _targetPosition; // ��ǥ ��ġ
    private Vector3 _startPosition; // ��ǥ ��ġ
    private float _startRotation; // ��ǥ ����
    private bool _isMoving = false;  // �̵� ������ ����
    private bool _isTurn = false;  // �̵� ������ ����

    public EnemyMove(EnemyBase enemy)
    {
        _enemy = enemy;
        _startPosition = _enemy.transform.position;     // ���ʹ��� �ʱ� ��ġ�� ���������� ����
        _startRotation = _enemy.transform.rotation.eulerAngles.z;
        _rigidbody2D = _enemy.GetComponent<Rigidbody2D>();
        SetRandomTargetPosition();                      // ó�� ��ǥ ��ġ ����
    }

    // Update is called once per frame
    public void Update(EnemyBase enemy)
    {
        if (_isTurn)
        {
            RotateTowardsTarget();

            // ȸ���� �Ϸ�Ǹ� �̵� ����
            if (Quaternion.Angle(_enemy.transform.rotation, Quaternion.Euler(0, 0, GetAngleToTarget() - 90)) < 0.1f)
            {
                _isTurn = false; // ȸ�� ����
                _isMoving = true; // �̵� ���·� ��ȯ
            }
        }

        if (_isMoving)
        {
            MoveTowardsTarget();
        }
    }


    private void SetRandomTargetPosition()
    {
        // ������ ������ ������ ����
        float randomAngle = Random.Range(0f, Mathf.PI * _enemy.MovementRadius);

        // ������ ���� x, y ��ǥ ���
        float randomX = Mathf.Cos(randomAngle) * _enemy.MovementRadius;
        float randomY = Mathf.Sin(randomAngle) * _enemy.MovementRadius;

        // ���� ��ġ���� ���� ��ǥ ����
        _targetPosition = _enemy.transform.position + new Vector3(randomX, randomY, 0);

        _isTurn = true; // ȸ�� ����
    }


    private void RotateTowardsTarget()
    {
        // ��ǥ ��ġ������ ���� ���
        Vector3 direction = _targetPosition - _enemy.transform.position;
        direction.z = 0f; // 2D ȯ���̹Ƿ� Z���� ȸ���� 0���� ����
        float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Z�� ȸ�� ���� ���

        // ��ǥ ȸ�� ����
        Quaternion targetRotation = Quaternion.Euler(0, 0, zRotation - 90); // Quaternion���� ��ȯ

        // ���� ȸ�� �������� ��ǥ ȸ�� ������ �ε巴�� ȸ��
        float angleDifference = Mathf.DeltaAngle(_enemy.transform.eulerAngles.z, zRotation - 90);
        float rotationAmount = Mathf.Clamp(angleDifference, -_enemy.RotationSpeed * 3 * Time.deltaTime, _enemy.RotationSpeed * 3 * Time.deltaTime);

        // ȸ�� ����
        _rigidbody2D.MoveRotation(_rigidbody2D.rotation + rotationAmount);
    }

    private void MoveTowardsTarget()
    {
        Vector2 targetPosition2D = new Vector2(_targetPosition.x, _targetPosition.y);

        float distance = Vector2.Distance(_rigidbody2D.position, targetPosition2D);

        if (distance > 0.5f)
        {
            Vector2 direction = (targetPosition2D - _rigidbody2D.position).normalized;
            _rigidbody2D.velocity = direction * _enemy.MoveSpeed;
        }
        else
        {
            //_rigidbody2D.velocity = Vector2.zero;
            _isMoving = false; // �̵� ����
            SetRandomTargetPosition(); // ���ο� ���� ��ġ ����
        }

        return;
    }

    private float GetAngleToTarget()
    {
        // ��ǥ ��ġ������ ���� ���
        Vector3 direction = _targetPosition - _enemy.transform.position;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Z�� ȸ�� ���� ��ȯ
    }
}
