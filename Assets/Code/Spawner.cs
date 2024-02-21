using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int spawn_time;
    public bool can_spawn;
    public GameObject Spawn_enemy;

    private void Start()
    {
        StartCoroutine(spawn());
    }
    // Update is called once per frame
    void Update()
    {
        
    }



    IEnumerator spawn()
    {
        Instantiate(Spawn_enemy, transform.position, transform.rotation);
        yield return new WaitForSeconds(spawn_time);
        StartCoroutine(spawn());
        yield return null;
    }
}
