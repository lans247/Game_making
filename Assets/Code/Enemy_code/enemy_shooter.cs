using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy_shooter : Enemy_normal       //�Ϲ� ���׹��� ��Ʈ���� ���
{
    public GameObject bullet;

    public override IEnumerator attack_move()            //enemy_normal�� �ִ� attack_move�� ���� �����
    {
        while (founded)
        {
            if (Vector2.Distance(transform.position, Player.transform.position) < info.view)     //�þߺ��� �Ÿ��� ������
            {
                Vector2 ve = (Player.transform.position - transform.position).normalized;     //������ �÷��̾�� ���ϴ� ����
                quaternion normal = quaternion.Euler(0, 0, Mathf.Atan2(ve.y, ve.x) - 3.14f);                //���� ����

                bullet.GetComponent<attack_normal>().master = "Enemy";
                bullet.GetComponent<attack_bullet>().damage = damage_cal(info.damage);
                Instantiate(bullet, transform.position, normal);

                yield return new WaitForSeconds(attack_speed);
            }
            yield return null;
        }
    }
}
