using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Map_manager : MonoBehaviour
{
    public List<map_info> management_map = new List<map_info>();       //�ڱⰡ �����ϴ� ���� �������� �� list
    public List<int> management_map_enemy_num = new List<int>();       //�����ϴ� ���� ���׹� ��, enemy info�� â���� ������ ������ �ڵ忡�� enemy���� ���� �� ���⿡ ���
    void Start()
    {
        initial();              //���� �������� �ʱ�ȭ��
    }

    public void initial()
    {
        for(int i = 0; i< management_map.Capacity; i++)
        {
            management_map_enemy_num.Add(management_map[i].enemy_num);      //�� ���� �Ѱ���
            switch(management_map[i].type)
            {
                case 'D':
                    management_map[i].clear = false;        //������ ��� ������ �� Ŭ���� ���θ� false�� ����
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

    public void enemy_died(map_info info)      //���� ������ �ʿ� �ִ� ���� ���� ���� ��Ŵ, enemy_normal���� ȣ��
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

        if (management_map_enemy_num[i] ==0 )           //�ش��ϴ� ���� �� ���ڰ� ���� ���
        {
            management_map[i].clear = true;             //Ŭ���� ó��.
        }
    }


    

}
