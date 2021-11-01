using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRayPoint : MonoBehaviour
{
    public GameObject bird;
    public bool IfMain = false;
    public bool ifBird = true;

    private void OnDrawGizmos()
    {
        if ((ifBird)&&(bird.GetComponent<Bird>().theOne))
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.position, bird.transform.position- transform.position);
        }
        else
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.position, bird.transform.position - transform.position);
        }

    }



}
