using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update

    private void Awake() //이동시 2개 이상이면 제거
    {
        var obj = FindObjectsOfType<MainCamera>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame

    Vector3 target_pos;
    void FixedUpdate()
    {
        target_pos = new Vector3(Player.transform.position.x, Player.transform.position.y, -20f) ;

        transform.position = Vector3.Lerp(transform.position, target_pos, Time.deltaTime*2f);
    }
}
