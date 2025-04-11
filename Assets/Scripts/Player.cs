using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// https://www.youtube.com/watch?v=whzomFgjT50&t=5s
// Movement code inspired by Brackeys
// Video uploaded August 11th, 2019


// https://www.youtube.com/watch?v=veFcxTNsfZY
// Flashing red code from ThatBoopGuy
// Video uploaded September 15th, 2020
public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public int health = 100;
    public int maxHealth = 100;
    public Animator animator;
    public Rigidbody2D rb;
    public Sword sword;
    public SpriteRenderer spriteRenderer;
    public GameObject restartButton;
    public UserInterface userInterface; 

    public float stepInterval = 0.2f;

    private float stepTimer = 0f;

    private Vector2 movement;
    private Vector2 lastMovementDirection = Vector2.right;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement.normalized;
            animator.SetFloat("Horizontal", lastMovementDirection.x);
            animator.SetFloat("Vertical", lastMovementDirection.y);

            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                if (audioManager != null)
                    audioManager.PlaySFX(audioManager.playerWalkClip);
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("AttackPressed");
            if (audioManager != null)
                audioManager.PlaySFX(audioManager.swingClip);
            Attack();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        sword.Attack(lastMovementDirection);
    }

    public void TakeDamage(int amount, float flashDuration = 0.1f)
    {
        health -= amount;
        Debug.Log("Player Health: " + health);
        if (audioManager != null)
            audioManager.PlaySFX(audioManager.playerHitClip);

        if (userInterface != null)
            StartCoroutine(userInterface.FlashRedHealthText(flashDuration));

        if (userInterface != null)
            StartCoroutine(userInterface.ShakeHealthText());

        if (health <= 0)
        {
            Debug.Log("Player is dead!");
            health = 0;
            if (audioManager != null)
            {
                audioManager.MuteAllExcept(audioManager.playerDeathClip);
            }

            StartCoroutine(ClearSceneAfterDeath());
        }

        if (spriteRenderer != null)
            StartCoroutine(FlashRed(flashDuration));
    }

    IEnumerator FlashRed(float duration)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;
    }

    IEnumerator ClearSceneAfterDeath()
    {
        DisablePlayerControlsAndVisuals();

        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        foreach (GameObject obj in fruits)
        {
            obj.SetActive(false);
        }

        if (audioManager != null)
        {
            audioManager.MuteAllExcept(audioManager.playerDeathClip);
        }

        if (restartButton != null)
            restartButton.SetActive(true);

        yield return null;
    }

    public void DisablePlayerControlsAndVisuals()
    {
        this.enabled = false;
        rb.velocity = Vector2.zero;
        rb.simulated = false;

        if (sword != null)
            sword.gameObject.SetActive(false);

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        if (animator != null)
            animator.enabled = false;

        foreach (Collider2D col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
