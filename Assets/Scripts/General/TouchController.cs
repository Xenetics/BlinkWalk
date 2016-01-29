using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{
    private Vector2 m_MouseDownPoint;
    private bool m_IsSwipe = false;

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_MouseDownPoint = Input.mousePosition;
            m_IsSwipe = true;
        }

        if (Input.GetMouseButtonUp(0) && m_IsSwipe)
        {
            if(Input.mousePosition.y > m_MouseDownPoint.y && RunnerPlayerController.Instance.grounded)
            {
                RunnerPlayerController.Instance.Jump();
            }
            else if(Input.mousePosition.y < m_MouseDownPoint.y)
            {
                RunnerPlayerController.Instance.Crouch();
            }
            m_IsSwipe = false;
        } 
    }
}
