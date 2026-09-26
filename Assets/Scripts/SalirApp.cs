using System.Collections;
using UnityEngine;

public class SalirApp : MonoBehaviour
{
    public float segundosDeEspera = 2f;

    private bool saliendo;

    public void SalirDelJuego()
    {
        if (saliendo) return;
        saliendo = true;
        StartCoroutine(SalirTrasEspera());
    }

    private IEnumerator SalirTrasEspera()
    {
        Debug.Log("Saliendo del juego...");
        yield return new WaitForSeconds(segundosDeEspera);

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}