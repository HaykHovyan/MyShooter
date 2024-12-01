using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedPU: MonoBehaviour
{
    [SerializeField]
    float yRotation;

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, yRotation++, 0);
    }
}
