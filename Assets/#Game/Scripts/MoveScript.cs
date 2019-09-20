using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float animationSpeedY = 3.0f;


    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float animationSpeedX = 1.0F;

    [SerializeField]
    [Range (0, 8)]
    private int xDestination = 1;

    [SerializeField]
    private float yDestination = -9;

    private void Start()
    {
        LeanTween.moveY(gameObject, yDestination, animationSpeedY).setLoopPingPong();
        LeanTween.moveX(gameObject,  xDestination + gameObject.transform.position.x, animationSpeedX).setEaseInOutCubic().setLoopPingPong();
        LeanTween.rotateZ(gameObject, 360, 5).setLoopCount(5);
    }

}
