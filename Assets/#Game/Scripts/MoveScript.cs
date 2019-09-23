using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private GameObject parent;

    private Vector3[] path;

    private Transform[] pathTransformers;

    private LTSpline visualizedPath;

    [SerializeField]
    private string pathLocation;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float speed = 3.0f;

    [SerializeField]
    private LeanTweenType loopType;

    [SerializeField]
    private LeanTweenType easeType;

    [SerializeField]
    private float startingTime = 0;

    [SerializeField]
    [Range(0, 8)]
    private int spriteRotationSpeed = 5;


    private void Awake()
    {
        parent = Resources.Load<GameObject>(pathLocation);
    }

    private void Start()
    {
        InitiateMovement();
    }

    public void InitiateMovement()
    {
        LeanTween.rotateAround(gameObject, Vector3.forward, 360, 6.0f).setLoopCount(10);
        pathTransformers = parent.GetComponentsInChildren<Transform>();
        path = new Vector3[pathTransformers.Length - 1];



        if (pathTransformers != null)
        {
            for (int i = 1; i < pathTransformers.Length; i++)
            {
                path[i - 1] = pathTransformers[i].position;
            }
        }

        if (path != null)
        {
            LTDescr tween = LeanTween.moveSpline(gameObject, path, speed)
                .setSpeed(speed)
                .setEase(easeType)
                .setLoopType(loopType)
                .setDelay(startingTime);
        }
    }

    public void SetParentPosition (float spawnInputX, float spawnInputY, float spawnInputZ)
    {
        parent = Resources.Load<GameObject>(pathLocation);
        parent.transform.position.Set(spawnInputX, spawnInputY, spawnInputZ);
    }
}
