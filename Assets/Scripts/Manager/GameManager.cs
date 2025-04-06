using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;
    private Transform player;

    public Checkpoint[] checkpoints;
    public string cloestCheckpointId;

    [Header("Lost Currecy")]
    [SerializeField] private GameObject lostcurrencyPrefab;
    public int lostCurrencyAmount;
    [SerializeField] private float lostCurrencyX;
    [SerializeField] private float lostCurrencyY;   
    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
        checkpoints = FindObjectsOfType<Checkpoint>();
    }

    private void Start()
    {
        player = PlayerManager.instance.player.transform;
    }
    public void RestartScene()
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        LoadLostCurrency(_data);

        foreach (KeyValuePair<string, bool> pair in _data.checkPoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.id == pair.Key && pair.Value)
                {
                    checkpoint.ActivateCheckpoint();
                    //  Debug.Log("Load Checkpoint");
                }
            }
        }

        cloestCheckpointId = _data.cloestCheckpointId;
        // Invoke("LoadCloestCheckPoint", 0.1f);
        LoadCloestCheckPoint();
    }

    private void LoadLostCurrency(GameData _data)
    {
        lostCurrencyAmount = _data.lostCurrencyAmount;
        lostCurrencyX = _data.lostCurrencyX;
        lostCurrencyY = _data.lostCurrencyY;
        if(lostCurrencyAmount > 0)
        {
            GameObject newLostcurrency = Instantiate(lostcurrencyPrefab, new Vector3(lostCurrencyX, lostCurrencyY), Quaternion.identity);
            newLostcurrency.GetComponent<LostCurrencyController>().currency = lostCurrencyAmount;
        }
        lostCurrencyAmount = 0;
    }

    public void SaveData(ref GameData _data)
    {
        _data.lostCurrencyX = player.position.x;
        _data.lostCurrencyY = player.position.y;
        _data.lostCurrencyAmount = lostCurrencyAmount;



        if (FindCloestCheckpoint() != null)
            _data.cloestCheckpointId = FindCloestCheckpoint().id;
        _data.checkPoints.Clear();

        foreach (Checkpoint checkpoint in checkpoints)
        {
            _data.checkPoints.Add(checkpoint.id, checkpoint.activationStatus);
           // Debug.Log("22");
        }
    }
    private void LoadCloestCheckPoint()
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (cloestCheckpointId == checkpoint.id)
            {
                player.position = checkpoint.transform.position;
            }

        }
    }
    private Checkpoint FindCloestCheckpoint()
    {
        float cloestDistance = Mathf.Infinity;
        Checkpoint cloestCheckpoint = null;

        foreach (Checkpoint checkpoint in checkpoints)
        {
            float distanceToCheckpoint = Vector2.Distance(player.position, checkpoint.transform.position);

            if (distanceToCheckpoint < cloestDistance && checkpoint.activationStatus)
            {
                cloestDistance = distanceToCheckpoint;
                cloestCheckpoint = checkpoint;
            }
        }
        return cloestCheckpoint;
    }

    public void PauseGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
   
}
