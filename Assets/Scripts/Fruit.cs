using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public SpriteRenderer spriteRenderer;

    private Animator animator;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    } 

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Fruit Health: " + health);
        if (audioManager != null)
            audioManager.PlaySFX(audioManager.fruitHitClip, 0.7f);
        if (spriteRenderer != null)
            StartCoroutine(FlashRed(.1f));
        if (health <= 0)
        {
            if (animator != null)
            {
                audioManager.PlaySFX(audioManager.fruitDeathClip);
                animator.SetTrigger("Explode");
            }
        }
    }

    public void DestroySelf()
    {
        Debug.Log("Fruit is destroyed!");
        Destroy(gameObject);
    }

    IEnumerator FlashRed(float duration)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;
    }
}
