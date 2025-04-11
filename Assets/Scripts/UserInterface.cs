using UnityEngine;
using TMPro;
using System.Collections;

public class UserInterface : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Player player;

    private Vector3 originalPosition;

    void Start()
    {
        if (healthText != null)
            originalPosition = healthText.transform.position;
    }

    void Update()
    {
        if (player != null && healthText != null)
        {
            healthText.text = "Health: " + player.health;
        }
    }

    public IEnumerator FlashRedHealthText(float duration)
    {
        healthText.color = Color.red;
        yield return new WaitForSeconds(duration);
        healthText.color = Color.white;
    }

    public IEnumerator ShakeHealthText()
    {
        float shakeDuration = 0.2f;
        float shakeMagnitude = 10f;

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float shakeX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float shakeY = Random.Range(-shakeMagnitude, shakeMagnitude);

            healthText.transform.position = originalPosition + new Vector3(shakeX, shakeY, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        healthText.transform.position = originalPosition;
    }
}
