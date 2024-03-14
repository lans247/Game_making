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
        if (collision.tag != master)       //���� ���, �̹� ���ݵ��� �ʾ��� ���
        {
            int hit_damage;                 //������
            Vector2 a;                      //��ġ�� ���� ����

            switch(collision.tag)
            {
                case "Enemy":
                    hit_damage = damage_cal(damage);            //������ ��Ʈ ������
                    a = (collision.transform.position - transform.position).normalized;        //���� ��ġ���� �� or �÷��̾� ��ġ���� ���͸� ����ȭ

                    collision.GetComponent<Enemy_normal>().HP -= hit_damage;

                    collision.GetComponent<Rigidbody2D>().AddForce(a * 10, ForceMode2D.Impulse);       //�� �������� ��ġ��
                    indicate(hit_damage, collision);
                    Destroy(this.gameObject);       //�Ѿ��� �浹�� �Ҹ�
                    break;
                case "Player":
                    hit_damage = damage_cal(damage);            //������ ��Ʈ ������
                    a = (collision.transform.position - transform.position).normalized;        //���� ��ġ���� �� or �÷��̾� ��ġ���� ���͸� ����ȭ

                    collision.GetComponent<Player_combat>().HP -= hit_damage;

                    collision.GetComponent<Rigidbody2D>().AddForce(a * 10, ForceMode2D.Impulse);       //�� �������� ��ġ��
                    indicate(hit_damage, collision);
                    Destroy(this.gameObject);       //�Ѿ��� �浹�� �Ҹ�
                    break;
                case "Shield":
                    transform.Rotate(0, 0, 180);               //���忡 ������ ����ٲ�
                    master = "Player";                         //����� �ٲ�
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

