using UnityEngine;
using UnityEngine.InputSystem;

namespace Utilities
{
    public static class MouseInput
    {
        #region GetWorldPositionByMouse
        public static Vector3 GetWorldPositionByMouse()
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(GetMousePositon()), out RaycastHit hit, Mathf.Infinity);
            return hit.point;
        }

        public static bool GetWorldPositionByMouse(out Vector3 position)
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(GetMousePositon()), out RaycastHit hit, Mathf.Infinity))
            {
                position = hit.point;
                return true;    
            }

            position = Vector3.zero;
            return false;
        }

        public static bool GetWorldPositionByMouse(out Vector3 position, out Collider collider)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(GetMousePositon()), out RaycastHit hit, Mathf.Infinity))
            {
                Ray ray = Camera.main.ScreenPointToRay(GetMousePositon());
                position = hit.point;
                collider = hit.collider;
                return true;
            }

            position = Vector3.zero;
            collider = null;
            return false;
        }

        public static Vector3 GetWorldPositionByMouse(Camera camera, float distance)
        {
            Physics.Raycast(camera.ScreenPointToRay(GetMousePositon()), out RaycastHit hit, Mathf.Infinity);
            return hit.point;
        }

        public static Vector3 GetWorldPositionByMouse(Camera camera, float distance, int layerMask)
        {
            Physics.Raycast(camera.ScreenPointToRay(GetMousePositon()), out RaycastHit hit, distance, layerMask);
            return hit.point;
        } 
        #endregion

        public static Vector3 GetMousePositon()
        {
            return Mouse.current.position.ReadValue();
        }
    }
}
