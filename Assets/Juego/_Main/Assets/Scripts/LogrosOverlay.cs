using UnityEngine;

public class LogrosOverlay : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject panelLogros;
    public LogrosMenuManager logrosManager;
    
    [Header("Configuración")]
    public KeyCode teclaAbrir = KeyCode.L;
    
    private bool estaAbierto = false;

    void Start()
    {
        // Asegurarse de que el panel esté oculto al inicio
        if (panelLogros != null)
        {
            panelLogros.SetActive(false);
        }
    }

    void Update()
    {
        // Detectar cuando se presiona la tecla L
        if (Input.GetKeyDown(teclaAbrir))
        {
            AlternarPanel();
        }
        
        // También permitir cerrar con ESC cuando está abierto
        if (estaAbierto && Input.GetKeyDown(KeyCode.Escape))
        {
            CerrarPanel();
        }
    }

    void AlternarPanel()
    {
        estaAbierto = !estaAbierto;
        
        if (panelLogros != null)
        {
            panelLogros.SetActive(estaAbierto);
            
            // Refrescar los logros cuando se abre
            if (estaAbierto && logrosManager != null)
            {
                logrosManager.RefrescarTodosLosLogros();
            }
            
            // Pausar el juego cuando se abre el panel
            Time.timeScale = estaAbierto ? 0f : 1f;
        }
    }

    public void CerrarPanel()
    {
        estaAbierto = false;
        
        if (panelLogros != null)
        {
            panelLogros.SetActive(false);
        }
        
        // Reanudar el juego
        Time.timeScale = 1f;
    }
}
