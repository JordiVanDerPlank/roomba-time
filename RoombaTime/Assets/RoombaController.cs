using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaController : MonoBehaviour
{
    public static RoombaController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!isCharging)
            charge -= Time.deltaTime;
    }

    [Header("Energy")]
    [SerializeField] float charge = 100;
    [SerializeField] float chargeSpeed = 1;
    bool isCharging;

    public void ChargeBattery()
    {
        isCharging = true;

        charge += chargeSpeed * Time.deltaTime;

        if (charge >= 100)
            charge = 100;
    }

    public void StopCharging()
    {
        isCharging = false;
    }

    [Header("Trash volume")]
    [SerializeField] float percentageFull;
    [SerializeField] float emptySpeed;

    public void EmptyTrash()
    {
        percentageFull -= emptySpeed * Time.deltaTime;
        if (percentageFull <= 0)
            percentageFull = 0;
    }

    public float GetPercentageFull()
    {
        return percentageFull;
    }

    public void AddTrashVolume(float percentage)
    {
        percentageFull += percentage;
        GameManager.Instance.AddScore((int)(percentage * 10));
    }
}
