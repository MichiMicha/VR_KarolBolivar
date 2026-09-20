using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ElementosManager : MonoBehaviour
{
    public static ElementosManager Instance;

    [Header("Cantidades")]
    [Tooltip("Cuántos elementos hay que tener en total para poder entregarlos a Celestia")]
    public int totalRequerido = 6;

    [Tooltip("Cuántos elementos ya tiene el jugador al empezar la escena")]
    public int elementosIniciales = 0;

    [Header("Interfaz (opcional)")]
    [Tooltip("Texto donde se muestra el contador")]
    public TMP_Text textoContador;

    [Tooltip("{0} = recolectados, {1} = total")]
    public string formatoContador = "Elementos: {0}/{1}";

    [Header("Eventos")]
    [Tooltip("Se dispara cada vez que el jugador recoge un elemento")]
    public UnityEvent OnElementoRecolectado;

    [Tooltip("Se dispara una sola vez, cuando ya tiene todos")]
    public UnityEvent OnTodosRecolectados;

    public int Recolectados { get; private set; }

    public bool TieneTodos
    {
        get { return Recolectados >= totalRequerido; }
    }

    public int Faltan
    {
        get { return Mathf.Max(0, totalRequerido - Recolectados); }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Recolectados = elementosIniciales;
    }

    void Start()
    {
        ActualizarTexto();
    }
    public void RegistrarElemento()
    {
        Recolectados++;
        ActualizarTexto();

        OnElementoRecolectado?.Invoke();

        if (Recolectados == totalRequerido)
        {
            OnTodosRecolectados?.Invoke();
        }
    }

    private void ActualizarTexto()
    {
        if (textoContador != null)
        {
            textoContador.text = string.Format(formatoContador, Recolectados, totalRequerido);
        }
    }
}

// Apoco si muy amistoso