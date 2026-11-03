using UnityEngine;
using TMPro;

public class MirarYRecolectar : MonoBehaviour {
  [Tooltip("Distancia máxima a la que hay que estar del objeto para seleccionarlo")]
  public float distanciaMaxima = 30f;
  [Tooltip("Segundos que hay que mirar el objeto para recogerlo")]
  public float segundosMirando = 1f;
  [Tooltip("Puntito de mira (opcional). Crece mientras se está seleccionando algo")]
  public Transform reticula;
  [Tooltip("Texto 3D que muestra cuántos objetos se han recogido (opcional)")]
  public TextMeshPro marcador;
  Component objetivo;
  float tiempo;
  int ultimoRecogidos = -1;
  Vector3 escalaInicial;

  void Start() {
    if (reticula != null) escalaInicial = reticula.localScale;
  }

  void Update() {
    Component mirado = null;
    if (Physics.Raycast(transform.position, transform.forward, out RaycastHit golpe, distanciaMaxima)) {
      mirado = golpe.collider.GetComponentInParent<Recolectable>();
      if (mirado == null) mirado = golpe.collider.GetComponentInParent<Recuperador>();
    }
    if (mirado != objetivo) { objetivo = mirado; tiempo = 0f; }
    if (objetivo != null) {
      tiempo += Time.deltaTime;
      if (tiempo >= segundosMirando) {
        if (objetivo is Recolectable r) r.Recolectar();
        else if (objetivo is Recuperador rec) rec.Activar(transform);
        objetivo = null;
        tiempo = 0f;
      }
    }
    if (reticula != null) {
      float progreso = objetivo != null ? tiempo / segundosMirando : 0f;
      reticula.localScale = escalaInicial * (1f + 2f * progreso);
    }
    ActualizarMarcador();
  }

  void ActualizarMarcador() {
    if (marcador == null) return;
    int total = Recolectable.Todos.Count;
    int recogidos = 0;
    foreach (var r in Recolectable.Todos)
      if (r.estado != Recolectable.Estado.Libre) recogidos++;
    if (recogidos == ultimoRecogidos) return;
    ultimoRecogidos = recogidos;
    marcador.text = (total > 0 && recogidos >= total) ? "¡Todo recogido!" : recogidos + " / " + total;
  }
}
