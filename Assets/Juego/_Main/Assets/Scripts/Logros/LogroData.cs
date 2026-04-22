using UnityEngine;

[System.Serializable]
public class LogroData
{
    public string id;
    public string titulo;
    [TextArea(2, 4)]
    public string descripcion;
    public Sprite iconoSprite;
}
