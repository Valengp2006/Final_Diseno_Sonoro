using UnityEngine;
using MoreMountains.CorgiEngine;

/// <summary>
/// Cambia la animación del Boss según su vida
/// Agrega este script al GameObject del Boss
/// </summary>
public class BossPhaseController : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("El Animator del Boss")]
    public Animator bossAnimator;
    
    [Header("Configuración de Fases")]
    [Tooltip("Vida total del Boss")]
    public float vidaMaxima = 100f;
    
    private Health health;
    private int faseActual = 1;

    void Start()
    {
        // Obtener componente de vida
        health = GetComponent<Health>();
        
        // Si no se asignó el Animator manualmente, buscarlo
        if (bossAnimator == null)
        {
            bossAnimator = GetComponent<Animator>();
        }
        
        // Iniciar en Fase 1
        if (bossAnimator != null)
        {
            bossAnimator.SetInteger("Phase", 1);
        }
    }

    void Update()
    {
        if (health == null || bossAnimator == null) return;
        
        // Calcular porcentaje de vida
        float porcentajeVida = (health.CurrentHealth / health.MaximumHealth) * 100f;
        
        // Determinar fase según vida
        int nuevaFase = CalcularFase(porcentajeVida);
        
        // Si cambió de fase, actualizar animación
        if (nuevaFase != faseActual)
        {
            faseActual = nuevaFase;
            bossAnimator.SetInteger("Phase", faseActual);
            Debug.Log($"Boss cambió a Fase {faseActual}");
        }
    }
    
    /// <summary>
    /// Calcula la fase según el porcentaje de vida
    /// </summary>
    int CalcularFase(float porcentajeVida)
    {
        if (porcentajeVida > 85f) return 1;      // 100-86%
        if (porcentajeVida > 70f) return 2;      // 85-71%
        if (porcentajeVida > 55f) return 3;      // 70-56%
        if (porcentajeVida > 40f) return 4;      // 55-41%
        if (porcentajeVida > 25f) return 5;      // 40-26%
        if (porcentajeVida > 10f) return 6;      // 25-11%
        return 7;                                 // 10-0%
    }
}
