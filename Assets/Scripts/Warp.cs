using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Warp : MonoBehaviour
{
    Transform nextTransform;
    public Action MissEnemy;

    public void SetNextTransform(Transform nextTransform) => this.nextTransform = nextTransform;

    void OnTriggerEnter(Collider other)
    {
        if (nextTransform == null)
        {
            MissEnemy?.Invoke();
            return;
        }

        other.transform.position = nextTransform.position;
        other.transform.rotation = Quaternion.Euler(Vector3.up * (nextTransform.rotation.eulerAngles.y + 90f));
    }
}
