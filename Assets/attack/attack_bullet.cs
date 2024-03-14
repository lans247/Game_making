using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class attack_bullet : attack_normal
{
    public void Update()
    {
        transform.Translate(-6 * Time.deltaTime, 0, 0);
    }

    public override IEnumerator disapear()
    {
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != master)       //적을 닿고, 이미 가격되지 않았을 경우
        {
            int hit_damage;                 //데미지
            Vector2 a;                      //밀치기 위한 벡터

            switch(collision.tag)
            {
                case "Enemy":
                    hit_damage = damage_cal(damage);            //실질적 히트 데미지
                    a = (collision.transform.position - transform.position).normalized;        //공격 위치에서 적 or 플레이어 위치까지 벡터를 정규화

                    collision.GetComponent<Enemy_normal>().HP -= hit_damage;

                    collision.GetComponent<Rigidbody2D>().AddForce(a * 10, ForceMode2D.Impulse);       //그 방향으로 밀치기
                    indicate(hit_damage, collision);
                    Destroy(this.gameObject);       //총알은 충돌시 소멸
                    break;
                case "Player":
                    hit_damage = damage_cal(damage);            //실질적 히트 데미지
                    a = (collision.transform.position - transform.position).normalized;        //공격 위치에서 적 or 플레이어 위치까지 벡터를 정규화

                    collision.GetComponent<Player_combat>().HP -= hit_damage;

                    collision.GetComponent<Rigidbody2D>().AddForce(a * 10, ForceMode2D.Impulse);       //그 방향으로 밀치기
                    indicate(hit_damage, collision);
                    Destroy(this.gameObject);       //총알은 충돌시 소멸
                    break;
                case "Shield":
                    transform.Rotate(0, 0, 180);               //쉴드에 맞으면 방향바꿈
                    master = "Player";                         //사용자 바뀜
                    break;
                case "Obstacle":
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
 
        }
        
    } 
}

