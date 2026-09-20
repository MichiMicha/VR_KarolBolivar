using UnityEngine;

public class MirarAlJugador : MonoBehaviour
{
    public bool soloEjeY = true;

    private Transform camara;

    void Start()
    {
        if (Camera.main != null) camara = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (camara == null) return;

        Vector3 direccion = transform.position - camara.position;
        if (soloEjeY) direccion.y = 0f;
        if (direccion.sqrMagnitude < 0.0001f) return;

        transform.rotation = Quaternion.LookRotation(direccion);
    }
}
