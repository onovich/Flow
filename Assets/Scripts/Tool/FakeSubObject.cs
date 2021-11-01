using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSubObject : MonoBehaviour
{
    public GameObject Father;

    // Update is called once per frame

    Transform subTrans;
    Transform fatherTrans;


    private void Start()
    {
        subTrans = GetComponent<Transform>();
        fatherTrans = Father.GetComponent<Transform>();

        

    }

    void FixedUpdate()
    {
        subTrans.localPosition = fatherTrans.localPosition;
        subTrans.localRotation = fatherTrans.localRotation;

    }
}
