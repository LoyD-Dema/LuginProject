using UnityEngine;
using UnityEngine.InputSystem;

namespace _TestDebug
{
    public class CursorLine : MonoBehaviour
    {
        public Camera camera;
        public Transform player;

        void Update()
        {
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.DrawLine(player.position, hit.point, Color.green);
            }
        }
    }
}