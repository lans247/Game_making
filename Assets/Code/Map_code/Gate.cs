using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField]
    Vector2 transfer;

    public map_info now_map;
    public map_info trans_map;
    

    public bool can_transfer = false;           //이동간 딜레이 제약을 위해 기본적으로 false, 이게 true일때 이동가능
    // Start is called before the first frame update

    private void Start()
    {
        InvokeRepeating("trans_check", 1f, 2f);      //3초마다 클리어 여부 체크
        GetComponent<SpriteRenderer>().color = Color.red;   //false상태일때 빨간색
    }

    public void trans_check()
    {
        can_transfer = now_map.clear;       //지금 있는 맵이 클리어 상태일때 통과가능
        if(can_transfer)        //이동가능 상태라면
        {
            GetComponent<SpriteRenderer>().color = Color.green;         //true상태일때 초록색
            CancelInvoke("trans_check");
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && can_transfer)
        {
            collision.transform.position = new Vector3(collision.transform.position.x + transfer.x, collision.transform.position.y + transfer.y, 0);
            collision.GetComponent<Player>().now_map = trans_map.map_name;            //맵 네임 넘겨주기
            //collision.GetComponent<Player>().map_range = map.map_range;            //맵 범위 넘겨주기
            GameObject.Find("UI").GetComponent<UI>().map_allert();          //맵 띄어주세요
        }
    }
}
