using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poi;
using UnityEngine;

public class Transform_Poi : iTransform
{
    private Transform transform;

    public Transform_Poi(Transform transform)
    {
        this.transform = transform;
    }
}
