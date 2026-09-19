using UnityEngine;
using TMPro;

/// <summary>
/// Muestra un texto de instrucciones que cambia por pasos.
/// Sirve para el tutorial y también para la sala del trono.
/// Los pasos avanzan llamando a IrAlPaso(n) desde eventos del Inspector
/// (TeleportPoint.OnTeleport, ElementosManager.OnElementoRecolectado, CelestiaNPC.alEntregar...).
/// Solo avanza, nunca retrocede: si un evento se repite (por ejemplo, teletransportarse varias veces)
/// no se rompe el orden.
/// </summary>
public class PanelInstrucciones : MonoBehaviour
{
    public TMP_Text textoInstruccion;

    [Tooltip("Un texto por paso. El paso 0 se muestra al empezar la escena.")]
    [TextArea(2, 5)] public string[] pasos;

    private int pasoActual = -1;

    void Start()
    {
        IrAlPaso(0);
    }

    public void IrAlPaso(int indice)
    {
        if (textoInstruccion == null || pasos == null) return;
        if (indice <= pasoActual || indice >= pasos.Length) return;

        pasoActual = indice;
        textoInstruccion.text = pasos[indice];
    }
}
