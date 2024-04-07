using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private void Awake()                //�̵��� 2�� �̻��̸� ����
    {
        var obj = FindObjectsOfType<Player>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
