﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECCameraShakeProjectile : MonoBehaviour {

    //public bool cameraShakeBool = true;
    public Animator CamerShakeAnimator;

    public void ShakeCamera()
    {
        CamerShakeAnimator.SetTrigger("CameraShakeTrigger");
    }
}
