using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_image : MonoBehaviour
{
    public int damage;

    public GameObject damage_indicate;      //가한 데미지 띄우기

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ex());
    }

    IEnumerator ex()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))       //적을 닿고, 이미 가격되지 않았을 경우
        {
            int hit_damage = damage_cal(damage);            //실질적 히트 데미지
            
            collision.collider.GetComponent<Enemy_normal>().HP -= hit_damage;

            Vector2 a = (collision.transform.position - transform.position).normalized;        //공격 위치에서 족 위치까지 벡터를 정규화
            collision.collider.GetComponent<Rigidbody2D>().AddForce(a * 40, ForceMode2D.Impulse);       //그 방향으로 밀칠기

            indicate(hit_damage, collision);
        }
    }

    public int damage_cal(int damage)       //실질적 데미지 계산
    {
        return damage;
    }

    public void indicate(int dama, Collision2D A)       //데미지 인디케이터 소환
    {
        damage_indicate.GetComponent<indicator>().type = "damage";
        damage_indicate.GetComponent<indicator>().content = dama;
        Instantiate(damage_indicate, A.transform.position, Quaternion.Euler(0, 0, 0));
    }

}
