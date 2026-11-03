using UnityEngine;
using UnityEngine.InputSystem;

public class CabezaEnEditor : MonoBehaviour {
  public float sensibilidad = 0.15f;
#if UNITY_EDITOR
  float giroX, giroY;
  void Update() {
    var raton = Mouse.current;
    if (raton == null || !raton.rightButton.isPressed) return;
    Vector2 d = raton.delta.ReadValue();
    giroY += d.x * sensibilidad;
    giroX = Mathf.Clamp(giroX - d.y * sensibilidad, -85f, 85f);
    transform.localRotation = Quaternion.Euler(giroX, giroY, 0f);
  }
#endif
}
