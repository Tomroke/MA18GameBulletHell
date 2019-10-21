using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    private Vector3[] path;

    private Transform[] pathTransformers;

    private LTSpline visualizePath;


    [Header("Private Variables")]
    [SerializeField]
    private GameObject pathParent; 

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float speed = 3.0f;

    [SerializeField]
    [Range(0, 8)]
    private int spriteRotationSpeed = 5;

    [Header("LeenTween Variables")]

    [SerializeField]
    private bool reversableMovement = false;

    [SerializeField]
    private LeanTweenType loopType;

    [SerializeField]
    private LeanTweenType easeType;

    private GameObject bossPathTwo;
    private int currentpath = 1;

    private void Start()
    {
        bossPathTwo = Resources.Load<GameObject>("Prefab/BossPath02");
        if (pathParent != null)
        {

            if (gameObject.tag.Equals("Boss"))
            {
                InitiateBossMovement();
            }
            else
            {
                InitiateMovement();
            }
        }

        LeanTween.rotateAround(gameObject, Vector3.forward, 360, 6.0f).setLoopCount(20);
    }


    public void InitiateMovement()
    {
        pathParent.transform.position = gameObject.transform.position;
        pathTransformers = pathParent.GetComponentsInChildren<Transform>();
        path = new Vector3[pathTransformers.Length - 1];

        if (pathTransformers != null)
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
                .setLoopType(loopType);
        }
    }


    private void InitiateBossMovement()
    {

        if (currentpath == 1)
        {
            currentpath++;
            pathTransformers = pathParent.GetComponentsInChildren<Transform>();
            path = new Vector3[pathTransformers.Length - 1];
        }
        else if (currentpath == 2)
        {
            currentpath--;
            pathTransformers = bossPathTwo.GetComponentsInChildren<Transform>();
            path = new Vector3[pathTransformers.Length - 1];
        }
        

        if (pathTransformers != null)
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
                .setOnComplete(InitiateBossMovement);
        }
    }

    public void ReverseEnemyPath()
    {
        if (reversableMovement)
        {
            pathParent.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        }
    }

    public void NormalEnemyPath()
    {
        if (reversableMovement)
        {
            pathParent.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
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

    //Draws the enemies path on screen when active.
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    if (visualizePath != null)
    //    {
    //        visualizePath.gizmoDraw();
    //    }
    //}
}
