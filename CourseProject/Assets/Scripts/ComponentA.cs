using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentA : MonoBehaviour
{
    void Awake()
    {
        MyTools.LOG(this, "Awake");
    }

    void OnEnable()
    {
        MyTools.LOG(this, "OnEnable");
    }

    void OnDisable()
    {
        MyTools.LOG(this, "OnDisable");
    }

    void OnDestroy()
    {
        MyTools.LOG(this, "OnDestroy");
    }

    // Start is called before the first frame update
    void Start()
    {
        MyTools.LOG(this, "Start");
    }

    // Update is called once per frame
    void Update()
    {
        MyTools.LOG(this, "Update");
    }

    void FixedUpdate()
    {
        MyTools.LOG(this, "FixedUpdate");
    }

    void LateUpdate()
    {
        MyTools.LOG(this, "LateUpdate");
    }
}
