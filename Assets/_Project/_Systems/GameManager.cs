using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;


    private void Awake()
    {
        CreateSingleton();
    }

    void CreateSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    public int amountOfPlayers = 1;
    public int level = 1;
    public bool gameStopped = true;
}
