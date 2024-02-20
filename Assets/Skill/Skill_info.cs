using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Skill_info : ScriptableObject
{
    public int id;
    public Sprite imagine;

    public int cooltime;
    public bool cool = true;
    public string Name;
}
