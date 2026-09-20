using UnityEngine;
using TMPro;

// Celestia luego de estar 1000 años gobernando y cansada de todo be like: *No hace nada*

public class PanelInstrucciones : MonoBehaviour
{
    public TMP_Text textoInstruccion;
    public string[] pasos;

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
