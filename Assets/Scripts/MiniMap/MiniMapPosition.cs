using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPosition : MonoBehaviour
{
    [SerializeField] private bool _x, _y, _z;       //true�� Ÿ���� ��ġ�� ���󰡰� false�� ���� ��ġ�� ����
    [SerializeField] private Transform _player;     //�̴ϸ��� �߽ɿ� ��ġ�� ������Ʈ

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
        {
            Debug.Log("�̴ϸ� �÷��̾� �� ã��");
            return;
        }
        Debug.Log("�̴ϸ�");
        transform.position = new Vector3(
            (_x ? _player.transform.position.x : transform.position.x), 
            (_y ? _player.transform.position.y : transform.position.y),
            (_z ? _player.transform.position.z : transform.position.z));
    }
}
