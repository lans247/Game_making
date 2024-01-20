using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update

    

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame

    Vector3 target_pos;
    void FixedUpdate()
    {
        target_pos = new Vector3(Player.transform.position.x, Player.transform.position.y, -20f) ;

        target_pos.x = Mathf.Clamp(target_pos.x, Player.GetComponent<Player>().map_range[0], Player.GetComponent<Player>().map_range[1]);
        target_pos.y = Mathf.Clamp(target_pos.y, Player.GetComponent<Player>().map_range[2], Player.GetComponent<Player>().map_range[3]);

        transform.position = Vector3.Lerp(transform.position, target_pos, Time.deltaTime*2f);
    }
}
