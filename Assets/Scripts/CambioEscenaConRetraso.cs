using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenaConRetraso : MonoBehaviour
{
    [Tooltip("Segundos que espera antes de cargar la escena (para que el jugador vea que llegó a la puerta)")]
    public float segundosDeEspera = 1.5f;

    private bool cargando;

    public void CargarEscena(int numeroEscena)
    {
        if (cargando) return;
        cargando = true;
        StartCoroutine(CargarTrasEspera(numeroEscena));
    }

    private IEnumerator CargarTrasEspera(int numeroEscena)
    {
        yield return new WaitForSeconds(segundosDeEspera);
        SceneManager.LoadScene(numeroEscena);
    }
}
