using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;   // Cambio pequeñito para no tener que maquillar nada y que se activen bien los eventos del tutorial cjbnss

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;
    public GameObject Player;
    private GameObject lastTeleportPoint;

    public UnityEvent OnTeleportado;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void DisableTeleportPoint(GameObject teleportPoint)
    {
        if (lastTeleportPoint != null)
        {
            lastTeleportPoint.SetActive(true);
        }

        teleportPoint.SetActive(false);
        lastTeleportPoint = teleportPoint;


#if UNITY_EDITOR
        Player.GetComponent<CardboardSimulator>().UpdatePlayerPositonSimulator();
#endif

        OnTeleportado?.Invoke();   // Pipipipipi
    }

}