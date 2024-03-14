using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour
{
    public map_info this_map;
    public int num;                 //num의 숫자, 이건 외부에서 적이 죽을 때마다 카운트 되는 걸로


    public void Awake()
    {
        this_map.clear = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (this_map.type)
        {
            case "town":        //마을
                this_map.clear = true;
                break;
            case "dungeon":     //던전
                this_map.clear = false;
                StartCoroutine(dungeon_check());
                break;
            case "Quest_where":
                this_map.clear = false;
                break;
        }
    }

    IEnumerator dungeon_check()         //던전의 클리어 여부 체크
    {
        yield return new WaitUntil(() => num == 0);
        this_map.clear = true;
    }
}
