using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Rotate : MonoBehaviour
{

    public float deltaRot = 10f; 

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0f, 0f, deltaRot * Time.deltaTime));       
    }
}
