using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerStatus : MonoBehaviour
{
    void Start()
    {
        Player.Instance.transform.position = Vector3.zero;
    }

}
