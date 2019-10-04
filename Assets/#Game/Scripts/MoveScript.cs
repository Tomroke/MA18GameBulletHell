using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    private Vector3[] path;

    private Transform[] pathTransformers;

    private LTSpline visualizePath;

    private bool reverseOrder = false;

    [Header("Private Variables")]
    [SerializeField]
    private GameObject pathParent; 

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float speed = 3.0f;

    [SerializeField]
    private float startingTime = 0;

    [SerializeField]
    [Range(0, 8)]
    private int spriteRotationSpeed = 5;

    [Header("LeenTween Variables")]
    [SerializeField]
    private LeanTweenType loopType;

    [SerializeField]
    private LeanTweenType easeType;
    private void Start()
    {
        pathParent.transform.position = gameObject.transform.position;
        InitiateMovement();
    }

    public void InitiateMovement()
    {
        LeanTween.rotateAround(gameObject, Vector3.forward, 360, 6.0f).setLoopCount(10);
        pathTransformers = pathParent.GetComponentsInChildren<Transform>();
        path = new Vector3[pathTransformers.Length - 1];

        if (pathTransformers != null && reverseOrder)
        {
            for (int i = pathTransformers.Length - 1; i > 1; i--)
            {
                path[path.Length - i] = pathTransformers[i].position;
            }
        }

        else if (pathTransformers != null)
        {
            for (int i = 1; i < pathTransformers.Length; i++)
            {
                path[i - 1] = pathTransformers[i].position;
            }
        }

        visualizePath = new LTSpline(path);

        if (path != null)
        {
            LTDescr tween = LeanTween.moveSpline(gameObject, path, speed)
                .setSpeed(speed)
                .setEase(easeType)
                .setLoopType(loopType)
                .setDelay(startingTime);
        }
    }

    public void SetParentPosition (Vector3 input)
    {
        pathParent.transform.position = input;
    }

    private void OnBecameInvisible()
    {
        if (gameObject.tag.Equals("Powerup"))
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (visualizePath != null)
        {
            visualizePath.gizmoDraw();
        }
    }
}
