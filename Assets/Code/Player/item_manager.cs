using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class item_manager : MonoBehaviour
{
    public List<Item_info> bag;
    public int item_num;
    public UI ui;                   //아이템 표시를 위한 UI가져오기.

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UI").GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void get_item(Item_info n){
        bag.Add(n);                             //플레이어 가방에 넣고
        ui.UI_inven_add(item_num);              //UI에 표시.
        item_num++;
        this.gameObject.GetComponent<Quest_manager>().update_progress(2, n.id);                //퀘스트 조건 충족 -> 넘김
    }
}
