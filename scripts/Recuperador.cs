using UnityEngine;

public class Recuperador : MonoBehaviour {
  [Tooltip("Segundos entre un objeto y el siguiente al salir")]
  public float separacion = 0.25f;
  [Tooltip("Altura sobre el objeto desde la que salen los recolectables")]
  public float alturaSalida = 1f;
  [Tooltip("Sonido al activarlo (opcional)")]
  public AudioClip sonido;
  public void Activar(Transform jugador) {
    int n = 0;
    Vector3 salida = transform.position + Vector3.up * alturaSalida;
    foreach (var r in Recolectable.Todos) {
      if (r.estado != Recolectable.Estado.Guardado) continue;
      r.Recuperar(jugador, salida, n * separacion);
      n++;
    }
    if (n > 0 && sonido != null) AudioSource.PlayClipAtPoint(sonido, transform.position);
  }
}
