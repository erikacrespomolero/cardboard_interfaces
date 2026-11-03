using System.Collections.Generic;
using UnityEngine;

public class Recolectable : MonoBehaviour {
  public enum Estado { Libre, Guardado, Volando, Entregado }
  public static readonly List<Recolectable> Todos = new List<Recolectable>();
  public float velocidadGiro = 90f;
  [Tooltip("Sonido al recogerlo (opcional)")]
  public AudioClip sonido;
  [Header("Al ser recuperado")]
  [Tooltip("Metros por segundo a los que vuela hacia el jugador")]
  public float velocidadVuelo = 6f;
  [Tooltip("A qué distancia del jugador se considera que ha llegado")]
  public float distanciaLlegada = 0.5f;
  [Tooltip("Si está marcado, al llegar vuelve a su sitio original y se puede recoger otra vez")]
  public bool reaparecerAlLlegar = false;
  public Estado estado { get; private set; } = Estado.Libre;
  Vector3 posicionInicial;
  Renderer[] renderizadores;
  Collider[] colliders;
  Transform destino;
  float retraso;
  void Awake() {
    posicionInicial = transform.position;
    renderizadores = GetComponentsInChildren<Renderer>();
    colliders = GetComponentsInChildren<Collider>();
  }

  void OnEnable() { Todos.Add(this); }
  void OnDisable() { Todos.Remove(this); }
  void Update() {
    if (estado == Estado.Libre) Girar();
    else if (estado == Estado.Volando) Volar();
  }
  void Girar() {
    transform.Rotate(0f, velocidadGiro * Time.deltaTime, 0f, Space.World);
  }

  void Volar() {
    if (retraso > 0f) { retraso -= Time.deltaTime; return; }
    Mostrar(true, false);
    Girar();
    Vector3 meta = destino.position + Vector3.down * 0.3f;
    transform.position = Vector3.MoveTowards(transform.position, meta, velocidadVuelo * Time.deltaTime);
    if (Vector3.Distance(transform.position, meta) < distanciaLlegada) Llegar();
  }

  void Llegar() {
    if (sonido != null) AudioSource.PlayClipAtPoint(sonido, transform.position);
    if (reaparecerAlLlegar) {
      transform.position = posicionInicial;
      estado = Estado.Libre;
      Mostrar(true, true);
    }
    else {
      estado = Estado.Entregado;
      Mostrar(false, false);
    }
  }

  public void Recolectar() {
    if (estado != Estado.Libre) return;
    if (sonido != null) AudioSource.PlayClipAtPoint(sonido, transform.position);
    estado = Estado.Guardado;
    Mostrar(false, false);
  }

  public void Recuperar(Transform jugador, Vector3 desde, float espera) {
    if (estado != Estado.Guardado) return;
    destino = jugador;
    retraso = espera;
    transform.position = desde;
    estado = Estado.Volando;
  }

  void Mostrar(bool visible, bool solido) {
    foreach (var r in renderizadores) r.enabled = visible;
    foreach (var c in colliders) c.enabled = solido;
  }
}
