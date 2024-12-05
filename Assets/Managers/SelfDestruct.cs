using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] GameObject destroyMe;

    void Start()
    {
        StartCoroutine(SelfDestroy());
        destroyMe.gameObject.SetActive(false);
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(30f);
        destroyMe.gameObject.SetActive(true);

        yield return new WaitForSeconds(7.5f);
        Destroy(destroyMe.gameObject);
    }
}
