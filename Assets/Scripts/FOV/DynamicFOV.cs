using System;
using UnityEngine;

public class DynamicFOV : FOV
{
    private void LateUpdate()
    {
        CreatFieldOfView();
    }
}
