using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    [SerializeField] float timeNeededToClean;
    [SerializeField] float timeCleaned;
    [SerializeField] float percentageSize;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (percentageSize + RoombaController.Instance.GetPercentageFull() > 100)
        {
            print("full");
            return;
        }

        if (other.tag == "Roomba")
            timeCleaned += Time.deltaTime;


        if (timeCleaned >= timeNeededToClean)
        {
            RoombaController.Instance.AddTrashVolume(percentageSize);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Roomba")
            timeCleaned = 0;
    }
}
