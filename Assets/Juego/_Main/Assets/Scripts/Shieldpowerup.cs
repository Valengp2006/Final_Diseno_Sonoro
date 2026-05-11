using System.Collections;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

public class ShieldPowerUp : MonoBehaviour
{
    [Header("Power-Up Settings")]
    [Tooltip("Duración del efecto del escudo en segundos")]
    public float shieldDuration = 10f;

    [Header("Visual Settings")]
    [Tooltip("Prefab visual del escudo (SpriteRenderer hijo del jugador)")]
    public Sprite shieldSprite;

    [Tooltip("Efecto de partículas al recoger (opcional)")]
    public GameObject pickupEffect;

    [Tooltip("Sonido al recoger (opcional)")]
    public AudioClip pickupSound;

    private bool hasBeenCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenCollected) return;

        Character character = collision.GetComponent<Character>();

        if (character != null && character.CharacterType == Character.CharacterTypes.Player)
        {
            Health health = character.GetComponent<Health>();

            // Evitar recoger otro si ya está invulnerable
            if (health != null && health.Invulnerable)
                return;

            ApplyPowerUp(character, health);
        }
    }

    private void ApplyPowerUp(Character character, Health health)
    {
        hasBeenCollected = true;

        // Curar 50% de la vida máxima
        if (health != null)
        {
            float healAmount = health.MaximumHealth * 0.5f;
            health.GetHealth(healAmount, character.gameObject);
        }

        //  Efectos visuales y sonido
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }

        if (pickupSound != null)
        {
            MMSoundManagerSoundPlayEvent.Trigger(
                pickupSound,
                MMSoundManager.MMSoundManagerTracks.Sfx,
                transform.position);
        }

        // Activar efecto temporal
        StartCoroutine(ShieldEffectCoroutine(character, health));

        // Destruir el ítem
        Destroy(gameObject);
    }

   private IEnumerator ShieldEffectCoroutine(Character character, Health health)
{
    if (health == null) yield break;

    health.Invulnerable = true;

    // Crear escudo visual
    GameObject shieldObject = null;

    if (shieldSprite != null)
    {
        shieldObject = new GameObject("ShieldVisual");
        shieldObject.transform.SetParent(character.transform);
        shieldObject.transform.localPosition = Vector3.zero;
        shieldObject.transform.localScale = Vector3.one * 0.5f;

        SpriteRenderer sr = shieldObject.AddComponent<SpriteRenderer>();
        sr.sprite = shieldSprite;
        sr.sortingOrder = 10;
    }

    float timer = 0f;

    while (timer < shieldDuration)
    {
        // Si murió → romper inmediatamente
        if (health.CurrentHealth <= 0)
        {
            break;
        }

        timer += Time.deltaTime;
        yield return null;
    }

    // FORZAR limpieza siempre
    if (health != null)
        health.Invulnerable = false;

    if (shieldObject != null)
        Destroy(shieldObject);
}

}