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

        /*void Update()
        {
            healthText.text = $"HP:{currentHealth}/{maxHealth}";
        }*/
        public void ChangeHealth(int change)
        {
            StartCoroutine(HurtAnimation());
            currentHealth += change;
            healthText.text = $"HP:{currentHealth}/{maxHealth}";
            healthTextAnim.Play("TextUpdate");
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        IEnumerator HurtAnimation()
        {
            playerSprite.color = Color.crimson;
            yield return new WaitForSeconds(0.2f);
            playerSprite.color =  Color.white;
        }
    }


