using easyar;
using UnityEngine;

public class LockTargetToScreen : MonoBehaviour
{
    ImageTargetController target;

    void Start()
    {
        target = GetComponent<ImageTargetController>();

        target.TargetFound += () =>
        {
            target.ActiveControl = TargetController.ActiveControlStrategy.None;
        };

        target.TargetLost += () =>
        {
            target.transform.GetChild(0).transform.localPosition = Vector3.forward;
        };
    }
}
