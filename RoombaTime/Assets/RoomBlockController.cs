using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrashSpawnPoint
{
    public bool IsAvailable = true;
    public Transform SpawnPoint;
}

public class RoomBlockController : MonoBehaviour
{
    [Header("Spawn delays")]
    [SerializeField] float minTimeBetweenSpawns;
    [SerializeField] float maxTimeBetweenSpawns;

    private void Start()
    {
        if (isEnabled)
            Invoke("SpawnTrash", Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
    }

    [Header("Room variables")]
    [SerializeField] bool isEnabled;
    [SerializeField] int percentageDirty;
    public void UpdatePercentageDirty()
    {
        percentageDirty = 0;
        foreach (var trash in trashInRoom)
        {
            percentageDirty += trash.GetPercentageSize();
        }
    }

    [SerializeField] List<TrashController> trashInRoom; 
    [SerializeField] TrashSpawnPoint[] trashSpawnPoints;

    public void SpawnTrash()
    {
        if (trashInRoom.Count < trashSpawnPoints.Length && GetAvailableSpawnSpoint() != null)
        {
            TrashSpawnPoint _spawnPoint = GetAvailableSpawnSpoint();

            TrashController _trashToSpawn = GameManager.Instance.GetRandomTrashPrefab().GetComponent<TrashController>();
            if ((percentageDirty + _trashToSpawn.GetPercentageSize()) <= 100)
            {
                TrashController _newTrash = Instantiate(_trashToSpawn, _spawnPoint.SpawnPoint.position, Quaternion.identity).GetComponent<TrashController>();
                trashInRoom.Add(_newTrash);
                _newTrash.SetRoom(this);
                _newTrash.SetSpawnPoint(_spawnPoint);
                _spawnPoint.IsAvailable = false;
                UpdatePercentageDirty();
            }
        }

        Invoke("SpawnTrash", Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
    }

    public void RemoveTrashFromRoom(TrashController trash)
    {
        trashInRoom.Remove(trash);
        SetSpawnPointAvailable(trash.GetSpawnPoint());
        UpdatePercentageDirty();
    }

    TrashSpawnPoint GetAvailableSpawnSpoint()
    {
        foreach (var spawnPoint in trashSpawnPoints)
        {
            if (spawnPoint.IsAvailable)
                return spawnPoint;
        }

        return null;
    }

    void SetSpawnPointAvailable(TrashSpawnPoint spawnPoint)
    {
        spawnPoint.IsAvailable = true;
    }

    [Header("Blocked room variables")]
    [SerializeField] int pointsRequiredToUnlock;
    public int GetPointsRequiredToUnlock()
    {
        return pointsRequiredToUnlock;
    }
    
    [SerializeField] Collider2D roomBlockedCollider;
    [SerializeField] SpriteRenderer roomBlockedSprite;
    public void UnlockRoom()
    {
        roomBlockedSprite.enabled = false;
        roomBlockedCollider.enabled = false;
        isEnabled = true;
        Invoke("SpawnTrash", Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
    }
}
