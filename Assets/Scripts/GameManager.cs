using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float preparationTime = 5f; // Temps de préparation avant la première vague
    public float timeBetweenWaves = 10f; // Temps d'attente entre les vagues
    public int startingGold = 100; // Or de départ du joueur
    public int playerHealth = 100; // Points de vie du joueur

    private int currentWave = 0; // Vague actuelle
    private int currentGold; // Or actuel du joueur

    // Singleton pattern pour accéder au GameManager depuis d'autres scripts
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // Assurez-vous qu'il n'y a qu'une seule instance du GameManager
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        currentGold = startingGold;
    }

    public void StartNextWave()
    {
        currentWave++;
        // Code pour démarrer la prochaine vague d'ennemis
    }

    public bool CanAfford(int cost)
    {
        // Vérifie si le joueur a suffisamment d'or pour acheter quelque chose
        return currentGold >= cost;
    }

    public void SpendGold(int amount)
    {
        // Dépense l'or spécifié par le joueur
        currentGold -= amount;
    }

    public void EarnGold(int amount)
    {
        // Gagne l'or spécifié par le joueur
        currentGold += amount;
    }

    public void ReducePlayerHealth(int damage)
    {
        // Réduit les points de vie du joueur
        playerHealth -= damage;
        // Vérifie si le joueur a perdu
        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        // Code pour gérer la fin du jeu lorsque le joueur a perdu
    }
}
