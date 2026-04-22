using UnityEngine;

[CreateAssetMenu(fileName = "NuevoLogro", menuName = "Logros/Crear Logro")]
[System.Serializable]
public class LogroData : ScriptableObject
{
    public string id;
    public string titulo;
    [TextArea(2, 4)]
    public string descripcion;
    public Sprite iconoSprite;
}
