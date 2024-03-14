using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField]
    Vector2 transfer;

    public map_info map;

    public bool can_transfer = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && can_transfer)
        {
            collision.transform.position = new Vector3(collision.transform.position.x + transfer.x, collision.transform.position.y + transfer.y, 0);
            collision.GetComponent<Player>().now_map = map.map_name;            //�� ���� �Ѱ��ֱ�
            collision.GetComponent<Player>().map_range = map.map_range;            //�� ���� �Ѱ��ֱ�
            GameObject.Find("UI").GetComponent<UI>().map_allert();          //�� ����ּ���
        }
    }
}
