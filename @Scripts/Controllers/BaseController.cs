using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BaseController : MonoBehaviour
{
    public ObjectType ObjectType { get; protected set; }

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {

    }

    void Update()
    {
        UpdateController();
    }

    public virtual void UpdateController()
    {

    }
}
