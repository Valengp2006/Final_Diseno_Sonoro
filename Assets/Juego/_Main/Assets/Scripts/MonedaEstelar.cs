using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

[AddComponentMenu("Luna Game/Coleccionables/Moneda Estelar")]
public class MonedaEstelar : PickableItem
{
    [Header("Configuración Moneda Estelar")]
    [Tooltip("Puntos que otorga al recolectarla")]
    public int puntosMoneda = 10;
    
    [Header("Audio Personalizado")]
    [Tooltip("Sonido de moneda estelar (opcional)")]
    public AudioClip sonidoMonedaEstelar;
    
    [Header("Efectos Visuales")]
    [Tooltip("Partículas al recolectar (opcional)")]
    public GameObject efectoParticulas;
    
    [Header("Animación de Rotación")]
    [Tooltip("Velocidad de rotación en grados por segundo")]
    public float velocidadRotacion = 100f;
    
    [Header("↕Animación de Flotación")]
    [Tooltip("Altura del movimiento de flotación")]
    public float amplitudFlotacion = 0.3f;
    
    [Tooltip("Velocidad de la flotación")]
    public float velocidadFlotacion = 2f;
    
    // Variables privadas para las animaciones
    private Vector3 posicionInicial;
    private float tiempoInicio;
    
    /// <summary>
    /// Inicialización - guarda posición inicial para la flotación
    /// </summary>
    protected override void Start()
    {
        base.Start(); // Llamar al Start de PickableItem
        
        posicionInicial = transform.position;
        tiempoInicio = Time.time;
    }
    
    /// <summary>
    /// Actualización cada frame - maneja las animaciones
    /// </summary>
    protected virtual void Update()
    {
        AnimarRotacion();
        AnimarFlotacion();
    }
    
    /// <summary>
    /// Rota la moneda continuamente en el eje Y
    /// </summary>
    void AnimarRotacion()
    {
        transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime, Space.World);
    }
    
    /// <summary>
    /// Hace que la moneda flote arriba y abajo suavemente
    /// </summary>
    void AnimarFlotacion()
    {
        float tiempoTranscurrido = Time.time - tiempoInicio;
        float nuevaY = posicionInicial.y + Mathf.Sin(tiempoTranscurrido * velocidadFlotacion) * amplitudFlotacion;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
    }
    
    /// <summary>
    /// Se ejecuta cuando el jugador recoge la moneda
    /// Sobreescribe el método de PickableItem
    /// </summary>
    protected override void Pick(GameObject picker)
    {
        // 1. Enviar puntos al sistema de Corgi (igual que Coin.cs original)
        CorgiEnginePointsEvent.Trigger(PointsMethods.Add, puntosMoneda);
        
        // 2. Reproducir sonido personalizado (si existe)
        if (sonidoMonedaEstelar != null)
        {
            MMSoundManagerSoundPlayEvent.Trigger(sonidoMonedaEstelar, 
                MMSoundManager.MMSoundManagerTracks.Sfx, 
                transform.position);
        }
        
        // 3. Crear efecto de partículas (si existe)
        if (efectoParticulas != null)
        {
            GameObject efecto = Instantiate(efectoParticulas, transform.position, Quaternion.identity);
            Destroy(efecto, 2f);
        }
        
        // 4. Llamar al Pick original de PickableItem (destruye el objeto, etc)
        base.Pick(picker);
    }
    
    /// <summary>
    /// Visualización en el editor para debugging
    /// </summary>
    void OnDrawGizmosSelected()
    {
        // Dibujar esfera amarilla para ver el área de la moneda
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        
        // Dibujar línea del rango de flotación
        Vector3 posInicial = Application.isPlaying ? posicionInicial : transform.position;
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        Gizmos.DrawLine(
            new Vector3(posInicial.x, posInicial.y - amplitudFlotacion, posInicial.z),
            new Vector3(posInicial.x, posInicial.y + amplitudFlotacion, posInicial.z)
        );
    }
}