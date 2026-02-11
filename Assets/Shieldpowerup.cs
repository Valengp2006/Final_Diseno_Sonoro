using System.Collections;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

public class ShieldPowerUp : MonoBehaviour
{
    [Header("Power-Up Settings")]
    [Tooltip("Cantidad de vida que se añade al recoger el power-up")]
    public int healthToAdd = 50;
    
    [Tooltip("Duración del efecto del escudo en segundos")]
    public float shieldDuration = 10f;
    
    [Tooltip("Porcentaje de reducción de daño (0.5 = 50% menos daño)")]
    [Range(0f, 1f)]
    public float damageReduction = 0.5f;
    
    [Header("Visual Settings")]
    [Tooltip("Sprite del personaje con escudo")]
    public Sprite shieldedSprite;
    
    [Tooltip("Escala del sprite con escudo (ajusta si se ve muy grande o pequeño)")]
    [Range(0.1f, 2f)]
    public float shieldSpriteScale = 1f;
    
    [Tooltip("Efecto de partículas al recoger (opcional)")]
    public GameObject pickupEffect;
    
    [Tooltip("Sonido al recoger (opcional)")]
    public AudioClip pickupSound;

    private bool hasBeenCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si quien toca es el personaje del jugador
        if (hasBeenCollected) return;
        
        Character character = collision.GetComponent<Character>();
        
        if (character != null && character.CharacterType == Character.CharacterTypes.Player)
        {
            ApplyPowerUp(character);
        }
    }

    private void ApplyPowerUp(Character character)
    {
        hasBeenCollected = true;

        //Aumentar la vida del personaje
        Health health = character.GetComponent<Health>();
        if (health != null)
        {
            health.GetHealth(healthToAdd, character.gameObject);
        }

        //Cambiar el sprite del personaje con escala ajustable
        SpriteRenderer characterSprite = character.GetComponentInChildren<SpriteRenderer>();
        Sprite originalSprite = null;
        Vector3 originalScale = Vector3.one;
        
        if (characterSprite != null && shieldedSprite != null)
        {
            originalSprite = characterSprite.sprite;
            originalScale = characterSprite.transform.localScale;
            
            // Cambiar sprite y aplicar la escala manual
            characterSprite.sprite = shieldedSprite;
            characterSprite.transform.localScale = originalScale * shieldSpriteScale;
        }

        // Iniciar el efecto temporal del escudo
        StartCoroutine(ShieldEffectCoroutine(character, health, characterSprite, originalSprite, originalScale));

        // Efectos visuales y de sonido
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }

        if (pickupSound != null)
        {
            MMSoundManagerSoundPlayEvent.Trigger(pickupSound, MMSoundManager.MMSoundManagerTracks.Sfx, transform.position);
        }

        // Destruir el power-up
        Destroy(gameObject);
    }

    private IEnumerator ShieldEffectCoroutine(Character character, Health health,
                                               SpriteRenderer characterSprite, Sprite originalSprite, Vector3 originalScale)
    {
        // Hacer al personaje temporalmente invulnerable o más resistente
        if (health != null)
        {
            // Activar invulnerabilidad temporal
            health.Invulnerable = true;
            
            // Opcional: Si no quieres invulnerabilidad total, puedes usar otro enfoque
            // Por ahora usamos invulnerabilidad para simular el escudo
        }

        // Esperar la duración del escudo
        yield return new WaitForSeconds(shieldDuration);

        // Restaurar el sprite original y su escala
        if (characterSprite != null && originalSprite != null)
        {
            characterSprite.sprite = originalSprite;
            characterSprite.transform.localScale = originalScale;
        }

        // Desactivar la invulnerabilidad
        if (health != null)
        {
            health.Invulnerable = false;
        }
    }

    // Visualización en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}