using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_normal : MonoBehaviour
{
    public string master;

    public int damage;

    public GameObject damage_indicate;      //가한 데미지 띄우기

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(disapear());
    }

    public virtual IEnumerator disapear()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != master)       //적을 닿고, 이미 가격되지 않았을 경우
        {
            int hit_damage = damage_cal(damage);            //실질적 히트 데미지
            Vector2 a = (collision.transform.position - transform.position).normalized;        //공격 위치에서 족 위치까지 벡터를 정규화

            if (master == "Enemy")
            {
                collision.GetComponent<Player_combat>().HP -= hit_damage;

            }
            else if (master == "Player")
            {
                collision.GetComponent<Enemy_normal>().HP -= hit_damage;
            }
            collision.GetComponent<Rigidbody2D>().AddForce(a * 40, ForceMode2D.Impulse);       //그 방향으로 밀칠기

            indicate(hit_damage, collision);
        }
    }

    public int damage_cal(int damage)       //실질적 데미지 계산
    {
        return damage;
    }

    public void indicate(int dama, Collider2D A)       //데미지 인디케이터 소환
    {
        damage_indicate.GetComponent<indicator>().type = "damage";
        damage_indicate.GetComponent<indicator>().content = dama;
        Instantiate(damage_indicate, A.transform.position, Quaternion.Euler(0, 0, 0));
    }

}
