using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingStationController : MonoBehaviour
{
    [SerializeField] float timeNeededToClean;
    [SerializeField] float timeCleaned;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Roomba")
            RoombaController.Instance.ChargeBattery();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Roomba")
            RoombaController.Instance.StopCharging();
    }
}
