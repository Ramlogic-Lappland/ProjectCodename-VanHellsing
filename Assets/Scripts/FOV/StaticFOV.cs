using UnityEngine;

public class StaticFOV : FOV
{
    protected override void Start()
    {
        base.Start();
        CreatFieldOfView();
    }
}
