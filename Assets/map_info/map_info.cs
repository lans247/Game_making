using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu]
public class map_info : ScriptableObject
{
    public string map_name;
    public float[] map_range = new float[4]; //-x, x, -y, x

    public string type;
    public bool clear;

}
