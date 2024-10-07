using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    [SerializeField] private Vector2 _mapSize;

    void Start()
    {
        CreateBoundary();
    }

    void CreateBoundary()
    {
        EdgeCollider2D edgeCollider = gameObject.AddComponent<EdgeCollider2D>();

        Vector2[] points = new Vector2[5];
        points[0] = new Vector2(-_mapSize.x / 2, -_mapSize.y / 2);  // ���ϴ�
        points[1] = new Vector2(_mapSize.x / 2, -_mapSize.y / 2);   // ���ϴ�
        points[2] = new Vector2(_mapSize.x / 2, _mapSize.y / 2);    // ����
        points[3] = new Vector2(-_mapSize.x / 2, _mapSize.y / 2);   // �»��
        points[4] = points[0];                                    // ���������� �ǵ��ƿ���

        edgeCollider.points = points;
    }
}
