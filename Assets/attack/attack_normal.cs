using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_normal : MonoBehaviour
{
    public string master;

    public int damage;

    public GameObject damage_indicate;      //���� ������ ����

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
        if (collision.tag != master)       //���� ���, �̹� ���ݵ��� �ʾ��� ���
        {
            int hit_damage = damage_cal(damage);            //������ ��Ʈ ������
            Vector2 a = (collision.transform.position - transform.position).normalized;        //���� ��ġ���� �� ��ġ���� ���͸� ����ȭ

            if (master == "Enemy")
            {
                collision.GetComponent<Player_combat>().HP -= hit_damage;

            }
            else if (master == "Player")
            {
                collision.GetComponent<Enemy_normal>().HP -= hit_damage;
            }
            collision.GetComponent<Rigidbody2D>().AddForce(a * 40, ForceMode2D.Impulse);       //�� �������� ��ĥ��

            indicate(hit_damage, collision);
        }
    }

    public int damage_cal(int damage)       //������ ������ ���
    {
        return damage;
    }

    public void indicate(int dama, Collider2D A)       //������ �ε������� ��ȯ
    {
        damage_indicate.GetComponent<indicator>().type = "damage";
        damage_indicate.GetComponent<indicator>().content = dama;
        Instantiate(damage_indicate, A.transform.position, Quaternion.Euler(0, 0, 0));
    }

}
