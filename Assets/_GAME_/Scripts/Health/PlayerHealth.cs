using _GAME_.Scripts.Player;
using TMPro;
using UnityEngine;

    public class PlayerHealth : MonoBehaviour
    {
        private int currentHealth;
        private int maxHealth;
        [SerializeField] private TextMeshProUGUI healthText;
        private Animator healthTextAnim;
        private PlayerPrefab player;
        void Start()
        {
            player = GetComponent<PlayerPrefab>();
            maxHealth = (int) player.maxHP;
            currentHealth = maxHealth;
            healthTextAnim = healthText.gameObject.GetComponent<Animator>();
            healthText.text = $"HP:{currentHealth}/{maxHealth}";
        }

        /*void Update()
        {
            healthText.text = $"HP:{currentHealth}/{maxHealth}";
        }*/
        public void ChangeHealth(int change)
        {
            currentHealth += change;
            healthText.text = $"HP:{currentHealth}/{maxHealth}";
            healthTextAnim.Play("TextUpdate");
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }


