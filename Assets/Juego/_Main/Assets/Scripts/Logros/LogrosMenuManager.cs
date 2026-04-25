using UnityEngine;

public class LogrosMenuManager : MonoBehaviour
{
    [Header("Prefab de Tarjeta de Logro")]
    public GameObject logroPrefab;
    
    [Header("Contenedor de Logros")]
    public Transform contenedorLogros;
    
    [Header("Popup de Logros")]
    public LogroPopup popupLogros;
    
    [Header("Datos de los 9 Logros")]
    public LogroData[] logros = new LogroData[9];

    void Start()
    {
        CrearTarjetasDeLogros();
    }

    void CrearTarjetasDeLogros()
    {
        // Verificar que tenemos el prefab y contenedor
        if (logroPrefab == null || contenedorLogros == null)
        {
            Debug.LogError("Falta asignar el prefab o contenedor de logros");
            return;
        }

        // Crear una tarjeta por cada logro
        for (int i = 0; i < logros.Length; i++)
        {
            if (logros[i] == null) continue;
            
            // Instanciar la tarjeta
            GameObject tarjetaObj = Instantiate(logroPrefab, contenedorLogros);
            
            // Obtener el componente LogroUI
            LogroUI tarjetaUI = tarjetaObj.GetComponent<LogroUI>();
            
            if (tarjetaUI != null)
            {
                // Inicializar con los datos y pasar referencia al popup
                tarjetaUI.Inicializar(logros[i], popupLogros);
            }
        }
    }

    // Método para refrescar todos los logros (útil si vuelves del juego)
    public void RefrescarTodosLosLogros()
    {
        LogroUI[] tarjetas = contenedorLogros.GetComponentsInChildren<LogroUI>();
        foreach (LogroUI tarjeta in tarjetas)
        {
            tarjeta.RefrescarEstado();
        }
    }
}