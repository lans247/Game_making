using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class item_normal : MonoBehaviour
{
    public Item_info item;
    private void Start()
    {
        transform.GetChild(0).Find("Name").GetComponent<TextMeshProUGUI>().text = item.title;    //������ �̸� ǥ��
        GetComponent<SpriteRenderer>().sprite = item.image;                                              //�׸� ǥ��
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<item_manager>().get_item(item);
            Destroy(this.gameObject);
        }
    }
}
