using UnityEngine;
using System.Collections;

public class ProximityTrigger2D : MonoBehaviour
{
    public int damageAmount = 5;
    public float damageInterval = 0.5f;
    public float moveSpeed = 2.0f;
    public Transform player;
    private bool playerInside = false;
    private Coroutine damageCoroutine;

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            if (damageCoroutine == null) 
            {
                damageCoroutine = StartCoroutine(DamagePlayer(other.GetComponent<Player>()));
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            playerInside = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    IEnumerator DamagePlayer(Player player)
    {
        while (playerInside && player.health > 0) 
        {
            player.TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
        damageCoroutine = null;
    }

}
