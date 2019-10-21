using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Material material;

    private Vector2 wallOffset;

    [SerializeField]
    [Range(-10.0f, 10.0f)]
    private float xVelocity, yVelocity;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    void Start()
    {
        wallOffset = new Vector2(xVelocity, yVelocity);
    }

    void Update()
    {
        material.mainTextureOffset += wallOffset * Time.deltaTime;
    }
}
