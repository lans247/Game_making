using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Map_manager : MonoBehaviour
{
    public List<map_info> management_map = new List<map_info>();       //자기가 관리하는 맵의 정보들이 들어갈 list
    public List<int> management_map_enemy_num = new List<int>();       //관리하는 맵의 에네미 수, enemy info의 창에서 수정할 지언정 코드에서 enemy수를 줄일 수 없기에 사용
    void Start()
    {
        initial();              //맵의 정보들을 초기화함
    }

    public void initial()
    {
        for(int i = 0; i< management_map.Capacity; i++)
        {
            management_map_enemy_num.Add(management_map[i].enemy_num);      //적 숫자 넘겨줌
            switch(management_map[i].type)
            {
                case 'D':
                    management_map[i].clear = false;        //던전의 경우 시작할 때 클리어 여부를 false로 시작
                    management_map_enemy_num[i] = management_map[i].enemy_num;         
                    break;
                case 'T':
                    management_map[i].clear = true;
                    break;
                case 'Q':
                    break;
            }
        }
    }

    public void enemy_died(map_info info)      //적이 죽으면 맵에 있는 적의 수를 감소 시킴, enemy_normal에서 호출
    {
        int i;
        for(i =0;i<management_map.Capacity;i++)
        {
            if(info == management_map[i])
            {
                management_map_enemy_num[i] -= 1;
                break;
            }
        }

        if (management_map_enemy_num[i] ==0 )           //해당하는 맵의 적 숫자가 없는 경우
        {
            management_map[i].clear = true;             //클리어 처리.
        }
    }


    

}
