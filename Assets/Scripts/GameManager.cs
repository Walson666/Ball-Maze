using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _curentLevel;
    public int CurentLevel => _curentLevel;

    [SerializeField] private List<Floor> floorsList;



    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RegisterFloorPieces();
    }

    public void LoadNextLevel()
    {
        DOTween.KillAll(true);
        _curentLevel++;
        if (_curentLevel >= SceneManager.sceneCountInBuildSettings) _curentLevel = 0;
        SceneManager.LoadScene(_curentLevel);

        
    }

    public void RegisterFloorPieces()
    {
        Floor[] floors = FindObjectsOfType<Floor>();
        floorsList = new List<Floor>(floors.Length);
        foreach(var floor in floors)
        {
            floorsList.Add(floor);
        }
    }

    public void UnregisterFloorPiece(Floor item)
    {
        floorsList.Remove(item);
        if (floorsList.Count == 0) LoadNextLevel();
    }
    
}
