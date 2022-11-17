using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    [SerializeField] float timeNeededToClean;
    [SerializeField] float timeCleaned;
    [SerializeField] int percentageSize;
    bool isCleaned;

    public int GetPercentageSize()
    {
        return percentageSize;
    }

    [SerializeField] RoomBlockController room;
    public void SetRoom(RoomBlockController room)
    {
        this.room = room;
    }

    public RoomBlockController GetRoom()
    {
        return room;
    }

    [SerializeField] TrashSpawnPoint spawnPoint;
    public void SetSpawnPoint(TrashSpawnPoint spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    public TrashSpawnPoint GetSpawnPoint()
    {
        return spawnPoint;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isCleaned)
            return;

        if (percentageSize + RoombaController.Instance.GetPercentageFull() > 100)
        {
            print("full");
            return;
        }

        if (other.tag == "Roomba")
            timeCleaned += Time.deltaTime;


        if (timeCleaned >= timeNeededToClean)
        {
            isCleaned = true;
            RoombaController.Instance.AddTrashVolume(percentageSize);
            room.RemoveTrashFromRoom(this);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Roomba")
            timeCleaned = 0;
    }
}
