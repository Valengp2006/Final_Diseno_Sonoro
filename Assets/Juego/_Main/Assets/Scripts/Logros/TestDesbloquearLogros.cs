using UnityEngine;

public class TestDesbloquearLogros : MonoBehaviour
{
    void Start()
    {
        // Desbloquear algunos logros para probar
        AchievementPersistence.Instance.ActivarLogro("nivel1_completado");
        AchievementPersistence.Instance.ActivarLogro("nivel2_completado");
        AchievementPersistence.Instance.ActivarLogro("50_monedas");
        AchievementPersistence.Instance.ActivarLogro("25_enemigos");
        
        Debug.Log("¡4 logros de prueba desbloqueados!");
    }
    
    // Puedes presionar la tecla U para desbloquear más logros mientras juegas
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            AchievementPersistence.Instance.ActivarLogro("100_monedas");
            Debug.Log("¡Logro adicional desbloqueado con tecla U!");
            
            // Refrescar la UI
            FindObjectOfType<LogrosMenuManager>().RefrescarTodosLosLogros();
        }
    }
}
