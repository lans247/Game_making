using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy_shooter : Enemy_normal       //일반 에네미의 컨트롤을 상속
{
    public GameObject bullet;

    public override IEnumerator attack_move()            //enemy_normal에 있는 attack_move를 뒤집 씌우기
    {
        while (founded)
        {
            if (Vector2.Distance(transform.position, Player.transform.position) < info.view)     //시야보다 거리가 가까우면
            {
                Vector2 ve = (Player.transform.position - transform.position).normalized;     //적에서 플레이어로 향하는 방향
                quaternion normal = quaternion.Euler(0, 0, Mathf.Atan2(ve.y, ve.x) - 3.14f);                //각도 방향

                bullet.GetComponent<attack_normal>().master = "Enemy";
                bullet.GetComponent<attack_bullet>().damage = damage_cal(info.damage);
                Instantiate(bullet, transform.position, normal);

                yield return new WaitForSeconds(attack_speed);
            }
            yield return null;
        }
    }
}
