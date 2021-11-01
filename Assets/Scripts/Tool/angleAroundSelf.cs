using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angleAroundSelf : MonoBehaviour
{
    [SerializeField]
    bool UpdateOnActive = false;
    GlobalCache global;

    bool ifScaled = false;

    private void Update()
    {
        if (UpdateOnActive)
        {
            if (ifScaled)
            {
                this.GetComponent<Transform>().Rotate(Vector3.forward, 20 * global.deltaTime, Space.World);

            }
            else
            {
                this.GetComponent<Transform>().Rotate(Vector3.forward, 20 * global.unscaledDeltaTime, Space.World);

            }


        }

    }

    private void Start()
    {
        global = GlobalCache.instance;
    }


    private void OnBecameVisible()
    {
        UpdateOnActive = true;
    }
    private void OnBecameInvisible()
    {
        UpdateOnActive = false;

    }

}
