using System.Collections;
using System.Collections.Generic;
using _GAME_.Scripts.Player;
using TMPro;
using UnityEngine;

    public class PlayerHealth : MonoBehaviour
    {
        private float currentHealth, maxHealth;
        [SerializeField] private TextMeshProUGUI healthText;
        private Animator healthTextAnim;
        private PlayerPrefab player;
        private Dictionary<UnitStat, float> statDict;
        private SpriteRenderer playerSprite;
        [SerializeField] private GameManager gameManager;
        void Start()
        {
            player = GetComponent<PlayerPrefab>();
            playerSprite = GetComponent<SpriteRenderer>();
            statDict = player.statDict;
            maxHealth = statDict.GetValueOrDefault(UnitStat.MaxHP);
            currentHealth = maxHealth;
            healthTextAnim = healthText.gameObject.GetComponent<Animator>();
            healthText.text = $"HP:{currentHealth}/{maxHealth}";
        }

        public void UpdateHealthUI()
        {
            maxHealth = statDict.GetValueOrDefault(UnitStat.MaxHP);
            healthText.text = $"HP:{currentHealth}/{maxHealth}";
        }
        /// <summary>
        /// Khi bị tấn công: thay đổi HP hiện tại, nếu cạn -> GameOver
        /// </summary>
        /// <param name="change"></param>
        public void ChangeHealth(int change)
        {
            if(Mathf.Sign(change) == -1)
                StartCoroutine(HurtAnimation());
            currentHealth += change;
            if(currentHealth > maxHealth)
                currentHealth = maxHealth;
            healthText.text = $"HP:{currentHealth}/{maxHealth}";
            healthTextAnim.Play("TextUpdate");
            if (currentHealth <= 0)
            {
                AudioSource.PlayClipAtPoint(player.audioManager.deathSFX, transform.position);
                gameManager.EnableGameOverUI(true);
            }
        }
        IEnumerator HurtAnimation()
        {
            playerSprite.color = Color.crimson;
            yield return new WaitForSeconds(0.2f);
            playerSprite.color =  Color.white;
        }
    }


