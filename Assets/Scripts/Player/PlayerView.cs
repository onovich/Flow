using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Test:Flow停留");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Flow触碰");
        if (other.CompareTag("Flow"))
        {
            other.GetComponent<Bird>().ifInPlayerView = true;
            
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Flow解除触碰");
        if (other.CompareTag("Flow"))
        {
            other.GetComponent<Bird>().ifInPlayerView = false;
        }
    }
}
