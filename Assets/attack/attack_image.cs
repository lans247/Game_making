using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_image : MonoBehaviour
{
    public int damage;

    public GameObject damage_indicate;      //���� ������ ����

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
        if(collision.collider.CompareTag("Enemy"))       //���� ���, �̹� ���ݵ��� �ʾ��� ���
        {
            int hit_damage = damage_cal(damage);            //������ ��Ʈ ������
            
            collision.collider.GetComponent<Enemy_normal>().HP -= hit_damage;

            Vector2 a = (collision.transform.position - transform.position).normalized;        //���� ��ġ���� �� ��ġ���� ���͸� ����ȭ
            collision.collider.GetComponent<Rigidbody2D>().AddForce(a * 40, ForceMode2D.Impulse);       //�� �������� ��ĥ��

            indicate(hit_damage, collision);
        }
    }

    public int damage_cal(int damage)       //������ ������ ���
    {
        return damage;
    }

    public void indicate(int dama, Collision2D A)       //������ �ε������� ��ȯ
    {
        damage_indicate.GetComponent<indicator>().type = "damage";
        damage_indicate.GetComponent<indicator>().content = dama;
        Instantiate(damage_indicate, A.transform.position, Quaternion.Euler(0, 0, 0));
    }

}
