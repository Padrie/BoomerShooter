using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Image healthBar;
    GameObject player;
    PlayerTestScript playerTestScript;

    int score;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        playerTestScript = player.GetComponent<PlayerTestScript>();
    }
    private void Update()
    {
        healthBar.fillAmount = Mathf.InverseLerp(0, playerTestScript.maxHealth, playerTestScript.currentHealth);
    }

    private void OnEnable()
    {
        EnemyStats.OnEnemyDied += AddScore;
    }

    private void OnDisable()
    {
        EnemyStats.OnEnemyDied -= AddScore;
    }

    public void AddScore()
    {
        if (score == 0) score = 1;
        scoreText.text = score++.ToString();
    }

}
