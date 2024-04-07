using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField]
    Vector2 transfer;

    public map_info now_map;
    public map_info trans_map;
    

    public bool can_transfer = false;           //�̵��� ������ ������ ���� �⺻������ false, �̰� true�϶� �̵�����
    // Start is called before the first frame update

    private void Start()
    {
        InvokeRepeating("trans_check", 1f, 2f);      //3�ʸ��� Ŭ���� ���� üũ
        GetComponent<SpriteRenderer>().color = Color.red;   //false�����϶� ������
    }

    public void trans_check()
    {
        can_transfer = now_map.clear;       //���� �ִ� ���� Ŭ���� �����϶� �������
        if(can_transfer)        //�̵����� ���¶��
        {
            GetComponent<SpriteRenderer>().color = Color.green;         //true�����϶� �ʷϻ�
            CancelInvoke("trans_check");
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && can_transfer)
        {
            collision.transform.position = new Vector3(collision.transform.position.x + transfer.x, collision.transform.position.y + transfer.y, 0);
            collision.GetComponent<Player>().now_map = trans_map.map_name;            //�� ���� �Ѱ��ֱ�
            //collision.GetComponent<Player>().map_range = map.map_range;            //�� ���� �Ѱ��ֱ�
            GameObject.Find("UI").GetComponent<UI>().map_allert();          //�� ����ּ���
        }
    }
}
