using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_wave : MonoBehaviour
{
    public int random;
    private void Start()
    {
        random = 1;
        if(Random.Range(0, 2) == 0)
        {
            random = 3;
        }

    }
    void FixedUpdate()
    {
        transform.Rotate(0, 0, random*250*Time.deltaTime);
        transform.position = GameObject.FindWithTag("Player").transform.position;
    }
}
