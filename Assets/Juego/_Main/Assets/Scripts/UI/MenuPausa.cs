using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [Header("🎮 Referencias UI")]
    public GameObject panelPausa;
    
    [Header("⚙️ Configuración")]
    public KeyCode teclaPausa = KeyCode.Escape;
    public string nombreEscenaMenu = "MenuPrincipal";
    
    private bool estaPausado = false;
    
    void Start()
    {
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
        Time.timeScale = 1f;
        estaPausado = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(teclaPausa))
        {
            if (estaPausado)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }
    }
    
    public void Pausar()
    {
        if (panelPausa != null)
        {
            panelPausa.SetActive(true);
        }
        Time.timeScale = 0f;
        estaPausado = true;
        Debug.Log("⏸️ Pausado");
    }
    
    public void Continuar()
    {
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
        Time.timeScale = 1f;
        estaPausado = false;
        Debug.Log("▶️ Continuado");
    }
    
    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("🏠 Ir al menú");
        SceneManager.LoadScene(nombreEscenaMenu);
    }
    
    public void AbrirOpciones()
    {
        Debug.Log("⚙️ Opciones");
        Continuar();
    }
}