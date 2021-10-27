using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public enum RotationAxis
{
    X,
    Y,
    Z
}

public enum UpdateBehaviour
{
    Update,
    FixedUpdate,
    LateUpdate
}

public class UltimateRotate : MonoBehaviour
{
    //Inspector Variables
    [SerializeField] private RotationAxis rotationAxis = default;
    [SerializeField] private UpdateBehaviour updateBehaviour = default;
    [SerializeField] private float rotationSpeed = 0.1f;
    public Transform customPivot = null;

    //Public properties
    public float RotationSpeed
    {
        get => rotationSpeed;
        set => rotationSpeed = value;
    }

    public RotationAxis RotationAxis
    {
        get => rotationAxis;
        set => rotationAxis = value;
    }


    private RotationAxis _previousRotationAxis;

    private Transform _chosenPivot;
    private Vector3 _rotationAxisVector3;

    private void Start()
    {
        _chosenPivot = customPivot ? customPivot : transform;
        SetRotationAxis();

        _previousRotationAxis = rotationAxis;
    }


    private void SetRotationAxis()
    {
        switch (rotationAxis)
        {
            case RotationAxis.X:
                _rotationAxisVector3 = transform.right;
                break;
            case RotationAxis.Y:
                _rotationAxisVector3 = transform.up;
                break;
            case RotationAxis.Z:
                _rotationAxisVector3 = transform.forward;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Rotate()
    {
        CheckVariables();
        PerformRotation();
    }

    private void PerformRotation()
    {
        transform.RotateAround(_chosenPivot.position, _rotationAxisVector3, rotationSpeed);
    }

    private void CheckVariables()
    {
        if (rotationAxis != _previousRotationAxis)
        {
            SetRotationAxis();

            _previousRotationAxis = rotationAxis;
        }
    }

    private void Update()
    {
        if (updateBehaviour == UpdateBehaviour.Update)
        {
            Rotate();
        }
    }

    private void FixedUpdate()
    {
        if (updateBehaviour == UpdateBehaviour.FixedUpdate)
        {
            Rotate();
        }
    }

    private void LateUpdate()
    {
        if (updateBehaviour == UpdateBehaviour.LateUpdate)
        {
            Rotate();
        }
    }
}