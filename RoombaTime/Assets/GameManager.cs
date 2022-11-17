using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FindAllRoomBlockers();
    }

    [SerializeField] int score;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreText();
    }

    public int GetScore()
    {
        return score;
    }

    [SerializeField] TextMeshProUGUI scoreText;

    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
        UpdateRoomBlockers();
    }

    [SerializeField] List<RoomBlockController> roomBlockers;

    void FindAllRoomBlockers()
    {
        roomBlockers = FindObjectsOfType<RoomBlockController>().ToList();
    }

    void RemoveRoomBlockerFromList(RoomBlockController roomBlocker)
    {
        roomBlockers.Remove(roomBlocker);
    }

    void UpdateRoomBlockers()
    {
        try
        {
            foreach (var roomBlocker in roomBlockers)
            {
                if (score >= roomBlocker.GetPointsRequiredToUnlock())
                {
                    RemoveRoomBlockerFromList(roomBlocker);
                    roomBlocker.UnlockRoom();
                }
            }
        }

        catch
        {
            return;
        }
    }

    [SerializeField] GameObject[] trashPrefabOptions;
    public GameObject GetRandomTrashPrefab()
    {
        return trashPrefabOptions[Random.Range(0, trashPrefabOptions.Length)];
    }
}
