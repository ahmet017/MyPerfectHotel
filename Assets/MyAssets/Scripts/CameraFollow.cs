﻿using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float distance;
    public float height;
    public float smoothness;

    public Transform camTarget;

    Vector3 velocity;
    public Vector3 offset;

    void LateUpdate()
    {
        if (!camTarget)
            return;
 
        Vector3 pos = Vector3.zero;
        pos.x = camTarget.position.x+offset.x;
        pos.y = camTarget.position.y + height;
        pos.z = camTarget.position.z - distance;

        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothness);
    }

    //public Material m;
    //public float TransparencyLevel;

    //private void Awake()
    //{
    //    m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
    //    m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    //    m.SetInt("_ZWrite", 0);
    //    m.DisableKeyword("_ALPHATEST_ON");
    //    m.DisableKeyword("_ALPHABLEND_ON");
    //    m.EnableKeyword("_ALPHAPREMULTIPLY_ON");
    //    m.renderQueue = 3000;

    //}
}
