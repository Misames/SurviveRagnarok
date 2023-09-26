using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject grid;
    public TextMeshProUGUI health;
    public TextMeshProUGUI gold;
    public float preparationTime = 5f; // Temps de préparation avant la première vague
    public float timeBetweenWaves = 10f; // Temps d'attente entre les vagues
    public uint startingGold = 100; // Or de départ du joueur
    public int playerHealth = 100; // Points de vie du joueur
    public GameObject[] buildings; // Liste de batiment disponible pour le niveau actuel
    private uint currentGold; // Or actuel du joueur

    // Singleton pattern pour accéder au GameManager depuis d'autres scripts
    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // Assurez-vous qu'il n'y a qu'une seule instance du GameManager
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        currentGold = startingGold;
        gold.text = currentGold.ToString();
        health.text = playerHealth.ToString();
        Time.timeScale = 1;
    }

    public bool CanAfford(uint cost)
    {
        // Vérifie si le joueur a suffisamment d'or pour acheter quelque chose
        return currentGold >= cost;
    }

    public void SpendGold(uint amount)
    {
        // Dépense l'or spécifié par le joueur
        currentGold -= amount;
        gold.text = currentGold.ToString();
    }

    public void EarnGold(uint amount)
    {
        // Gagne l'or spécifié par le joueur
        currentGold += amount;
        gold.text = currentGold.ToString();
    }

    public void ReducePlayerHealth(int damage)
    {
        // Réduit les points de vie du joueur
        playerHealth -= damage;
        health.text = playerHealth.ToString();

        // Vérifie si le joueur a perdu
        if (playerHealth <= 0)
        {
            gameOverMenu.SetActive(true);
            grid.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void RetryLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
