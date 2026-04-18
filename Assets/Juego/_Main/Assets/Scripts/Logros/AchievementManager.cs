using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

public class AchievementManager : MonoBehaviour, MMEventListener<CorgiEngineEvent>, MMEventListener<CorgiEnginePointsEvent>, MMEventListener<MMDamageTakenEvent>
{
    // Nos suscribimos a los eventos de Corgi
    void OnEnable() 
    { 
        this.MMEventStartListening<CorgiEngineEvent>(); 
        this.MMEventStartListening<CorgiEnginePointsEvent>(); 
        this.MMEventStartListening<MMDamageTakenEvent>(); 
    }
    void OnDisable() 
    { 
        this.MMEventStopListening<CorgiEngineEvent>(); 
        this.MMEventStopListening<CorgiEnginePointsEvent>(); 
        this.MMEventStopListening<MMDamageTakenEvent>(); 
    }

    // 1. EVENTOS DE HISTORIA Y ESTADO (Niveles y Héroe Perfecto)
    public void OnMMEvent(CorgiEngineEvent engineEvent)
    {
        if (engineEvent.EventType == CorgiEngineEventTypes.LevelComplete)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            
            if (sceneName == "Nivel1") AchievementPersistence.Instance.ActivarLogro("nivel1_completado");
            if (sceneName == "Nivel2") AchievementPersistence.Instance.ActivarLogro("nivel2_completado");
            if (sceneName == "Nivel3") 
            {
                AchievementPersistence.Instance.ActivarLogro("nivel3_completado");
                
                // Verificamos si no ha muerto nunca para el logro final
                if (PlayerPrefs.GetInt("muertes_luna", 0) == 0)
                {
                    AchievementPersistence.Instance.ActivarLogro("sin_morir");
                }
            }
        }
        
        // Registrar si Luna muere para anular "Héroe Perfecto"
        if (engineEvent.EventType == CorgiEngineEventTypes.PlayerDeath)
        {
            int muertes = PlayerPrefs.GetInt("muertes_luna", 0) + 1;
            PlayerPrefs.SetInt("muertes_luna", muertes);
        }
    }

    // 2. EVENTOS DE COLECCIÓN (Monedas)
    public void OnMMEvent(CorgiEnginePointsEvent pointsEvent)
    {
        int totalMonedas = GameManager.Instance.Points;
        
        if (totalMonedas >= 50) AchievementPersistence.Instance.ActivarLogro("50_monedas");
        if (totalMonedas >= 100) AchievementPersistence.Instance.ActivarLogro("100_monedas");
        if (totalMonedas >= 165) AchievementPersistence.Instance.ActivarLogro("todas_monedas");
    }

    // 3. EVENTOS DE COMBATE (Enemigos Derrotados)
    public void OnMMEvent(MMDamageTakenEvent damageEvent)
    {
        // 1. Obtenemos el componente de salud directamente del evento
        Health healthComponent = damageEvent.AffectedHealth;

        // 2. Verificamos que no sea nulo y que la salud haya bajado a 0 (o menos)
        if (healthComponent != null && healthComponent.CurrentHealth <= 0)
        {
            // 3. Validamos que el objeto muerto NO tenga el tag del jugador
            // (Asegúrate de que Luna tiene asignado el tag "Player" en el inspector de Unity)
            if (!healthComponent.gameObject.CompareTag("Player"))
            {
                // Registramos 1 baja. Si llega a 25 o 50, dispara los logros.
                AchievementPersistence.Instance.RegistrarProgreso("enemigos_derrotados", 1, 25, "25_enemigos");
                AchievementPersistence.Instance.RegistrarProgreso("enemigos_derrotados", 1, 50, "50_enemigos");
            }
        }
    }
}