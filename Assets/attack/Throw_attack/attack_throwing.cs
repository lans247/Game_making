using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_throwing : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Translate(-15*Time.deltaTime, 0, 0);
    }
}
