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
        map_confiner = GameObject.Find("map_confiner").GetComponent<PolygonCollider2D>();
    }
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = player;
        GetComponent<CinemachineVirtualCamera>().LookAt = player;

        GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = map_confiner;
    }
}
