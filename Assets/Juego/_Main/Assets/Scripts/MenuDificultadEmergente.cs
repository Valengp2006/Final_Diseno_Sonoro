using UnityEngine;

public class MenuDificultadEmergente : MonoBehaviour
{
    [Header("UI Elementos")]
    [Tooltip("Arrastra aquí el Panel que contiene los botones de dificultad")]
    public GameObject panelDificultad; 

    [Header("Configuración")]
    [Tooltip("La tecla que abrirá el menú")]
    public KeyCode teclaMenu = KeyCode.F; // Tecla 'F' por defecto

    private bool juegoPausado = false;

    void Start()
    {
        // Nos aseguramos de que la ventana esté oculta al iniciar el nivel
        if (panelDificultad != null)
        {
            panelDificultad.SetActive(false);
        }
    }

    void Update()
    {
        // Detecta si se presiona la tecla configurada (F)
        if (Input.GetKeyDown(teclaMenu))
        {
            AlternarMenuDificultad();
        }
    }

    public void AlternarMenuDificultad()
    {
        juegoPausado = !juegoPausado;
        
        if (juegoPausado)
        {
            panelDificultad.SetActive(true); // Muestra la ventana emergente
            Time.timeScale = 0f;             // Pausa el tiempo en Unity
        }
        else
        {
            ReanudarJuego(); // Si se vuelve a presionar la tecla o el botón "Atrás", se cierra
        }
    }

    // --- Funciones para los botones de dificultad ---

    public void SeleccionarFacil()
    {
        PlayerPrefs.SetInt("DificultadJuego", 0);
        PlayerPrefs.Save();
        NotificarAEnemigos(); // Avisa a los enemigos del cambio inmediatamente
        ReanudarJuego();
    }

    public void SeleccionarMedio()
    {
        PlayerPrefs.SetInt("DificultadJuego", 1);
        PlayerPrefs.Save();
        NotificarAEnemigos(); // Avisa a los enemigos del cambio inmediatamente
        ReanudarJuego();
    }

    public void SeleccionarDificil()
    {
        PlayerPrefs.SetInt("DificultadJuego", 2);
        PlayerPrefs.Save();
        NotificarAEnemigos(); // Avisa a los enemigos del cambio inmediatamente
        ReanudarJuego();
    }

    // --- Funciones de control interno ---

    private void NotificarAEnemigos()
    {
        // Busca a todos los enemigos en la pantalla y les dice que actualicen sus stats en tiempo real
        AjusteDificultadCorgi[] enemigosEnPantalla = FindObjectsOfType<AjusteDificultadCorgi>();
        foreach (AjusteDificultadCorgi enemigo in enemigosEnPantalla)
        {
            enemigo.AplicarDificultadCorgi();
        }
    }

    private void ReanudarJuego()
    {
        juegoPausado = false;
        if (panelDificultad != null)
        {
            panelDificultad.SetActive(false); // Oculta la ventana
        }
        Time.timeScale = 1f;                  // El tiempo vuelve a la normalidad
    }
}