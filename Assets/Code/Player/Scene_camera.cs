using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_camera : MonoBehaviour
{
    [SerializeField]
    Transform player;

    PolygonCollider2D map_confiner;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        map_confiner = GameObject.Find("map_confiner").GetComponent<PolygonCollider2D>();       //맵 제한자.
    }
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = player;                               //플레이어 따라다니고
        GetComponent<CinemachineVirtualCamera>().LookAt = player;                               //플레이어를 바라봄

        GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = map_confiner;     
    }
}
