using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Collider2D swordCollider;
    public SpriteRenderer swordSprite;
    public Animator animator;
    public int damage = 100;
    public float attackDuration = 0.3f; // Measured in seconds

    private void Start()
    {
        swordSprite.enabled = false;
        swordCollider.enabled = false;
    }

    public void Attack(Vector2 direction)
    {
        swordCollider.enabled = true;
        swordSprite.enabled = true;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetTrigger("AttackPressed");

        AdjustSwordPosition(direction);
        StartCoroutine(DisableColliderAfterTime());
    }

    private IEnumerator DisableColliderAfterTime()
    {
        yield return new WaitForSeconds(attackDuration);
        swordCollider.enabled = false;
        swordSprite.enabled = false;
    }

    private void AdjustSwordPosition(Vector2 direction)
    {
        float distanceToPlayer = 0.4f;
        if (direction.x > 0)
            transform.localPosition = new Vector3(distanceToPlayer, 0f, 0f);
        else if (direction.x < 0)
            transform.localPosition = new Vector3(-distanceToPlayer, 0f, 0f);
        else if (direction.y > 0)
            transform.localPosition = new Vector3(0f, distanceToPlayer, 0f);
        else if (direction.y < 0)
            transform.localPosition = new Vector3(0f, -distanceToPlayer, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Fruit"))
        {
            Fruit fruit = collision.GetComponent<Fruit>();
            fruit.TakeDamage(damage);
            
        }
    }
}
