using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    [SerializeField] float timeNeededToClean;
    [SerializeField] float timeCleaned;

    private void OnTriggerStay2D(Collider2D other)
    {
        print(other.tag);

        if (other.tag == "Roomba")
            timeCleaned += Time.deltaTime;
        
        
        if (timeCleaned >= timeNeededToClean)
            Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        print(other.tag + " left trigger");

        if (other.tag == "Roomba")
            timeCleaned = 0;
    }
}
