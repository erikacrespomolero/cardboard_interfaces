using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovimientoAvatar : MonoBehaviour {
  public Transform camara;
  public float velocidad = 2f;
  [Header("Moverse con la vista")]
  public bool moverConLaVista = true;
  [Tooltip("A partir de cuántos grados mirando hacia abajo se empieza a caminar")]
  public float anguloMinimo = 30f;
  [Tooltip("Si se mira más abajo que esto, se deja de caminar")]
  public float anguloMaximo = 70f;
  CharacterController controlador;
  float velocidadVertical;
  void Start() {
    controlador = GetComponent<CharacterController>();
    if (camara == null && Camera.main != null) camara = Camera.main.transform;
  }
  void Update() {
    Vector3 adelante = camara.forward; adelante.y = 0f; adelante.Normalize();
    Vector3 derecha = camara.right; derecha.y = 0f; derecha.Normalize();
    Vector2 entrada = Vector2.zero;
    if (Gamepad.current != null) entrada = Gamepad.current.leftStick.ReadValue();
    if (Keyboard.current != null) {
      if (Keyboard.current.wKey.isPressed) entrada.y += 1f;
      if (Keyboard.current.sKey.isPressed) entrada.y -= 1f;
      if (Keyboard.current.dKey.isPressed) entrada.x += 1f;
      if (Keyboard.current.aKey.isPressed) entrada.x -= 1f;
    }
    Vector3 movimiento = adelante * entrada.y + derecha * entrada.x;
    if (movimiento.sqrMagnitude < 0.01f && moverConLaVista) {
      float inclinacion = camara.eulerAngles.x;
      if (inclinacion > 180f) inclinacion -= 360f; 
      if (inclinacion > anguloMinimo && inclinacion < anguloMaximo) movimiento = adelante;
    }
    movimiento = Vector3.ClampMagnitude(movimiento, 1f) * velocidad;
    if (controlador.isGrounded) velocidadVertical = -1f;
    else velocidadVertical += Physics.gravity.y * Time.deltaTime;
    movimiento.y = velocidadVertical;
    controlador.Move(movimiento * Time.deltaTime);
  }
}
