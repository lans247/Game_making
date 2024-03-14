using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour
{
    public map_info this_map;
    public int num;                 //num�� ����, �̰� �ܺο��� ���� ���� ������ ī��Ʈ �Ǵ� �ɷ�


    public void Awake()
    {
        this_map.clear = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (this_map.type)
        {
            case "town":        //����
                this_map.clear = true;
                break;
            case "dungeon":     //����
                this_map.clear = false;
                StartCoroutine(dungeon_check());
                break;
            case "Quest_where":
                this_map.clear = false;
                break;
        }
    }

    IEnumerator dungeon_check()         //������ Ŭ���� ���� üũ
    {
        yield return new WaitUntil(() => num == 0);
        this_map.clear = true;
    }
}
