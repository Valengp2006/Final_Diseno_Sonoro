using UnityEngine;
using MoreMountains.CorgiEngine; 

public class AjusteDificultadCorgi : MonoBehaviour
{
    [Header("Configuración de Salud del Enemigo")]
    [Tooltip("La cantidad de vida en cada dificultad")]
    public float saludFacil = 20f; 
    public float saludMedia = 50f;
    public float saludDificil = 100f;

    [Header("Configuración de Daño al Jugador")]
    [Tooltip("El daño que hace al tocar a Luna")]
    public float danoFacil = 5f; 
    public float danoMedio = 15f;
    public float danoDificil = 25f; 

    void Start()
    {
        AplicarDificultadCorgi();
    }

    public void AplicarDificultadCorgi()
    {
        // Leemos la dificultad guardada desde el menú (0=Fácil, 1=Medio, 2=Difícil)
        int nivelDificultad = PlayerPrefs.GetInt("DificultadJuego", 1);

        // 1. MODIFICAR LA SALUD (Buscamos en el objeto principal)
        Health componenteSalud = GetComponent<Health>();
        if (componenteSalud != null)
        {
            if (nivelDificultad == 0) componenteSalud.MaximumHealth = saludFacil;
            else if (nivelDificultad == 1) componenteSalud.MaximumHealth = saludMedia;
            else if (nivelDificultad == 2) componenteSalud.MaximumHealth = saludDificil;
            
            // Actualizamos la salud actual para que empiece con el máximo
            componenteSalud.CurrentHealth = componenteSalud.MaximumHealth; 
            componenteSalud.UpdateHealthBar(false); 
        }

        // 2. MODIFICAR EL DAÑO (Buscamos en el objeto y en todos sus "hijos")
        // Usamos un array [] por si el enemigo tiene varias zonas de daño
        DamageOnTouch[] componentesDano = GetComponentsInChildren<DamageOnTouch>();
        
        foreach (DamageOnTouch zonaDeDano in componentesDano)
        {
            if (nivelDificultad == 0) 
            {
                zonaDeDano.MinDamageCaused = danoFacil;
                zonaDeDano.MaxDamageCaused = danoFacil;
            }
            else if (nivelDificultad == 1) 
            {
                zonaDeDano.MinDamageCaused = danoMedio;
                zonaDeDano.MaxDamageCaused = danoMedio;
            }
            else if (nivelDificultad == 2) 
            {
                zonaDeDano.MinDamageCaused = danoDificil;
                zonaDeDano.MaxDamageCaused = danoDificil;
            }
        }
    }
}