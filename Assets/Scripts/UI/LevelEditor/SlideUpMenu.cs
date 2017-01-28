using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUpMenu : MonoBehaviour
{
    private RectTransform m_MenuRect;
    [SerializeField]
    private GameObject m_Arrow;
    private Vector3 m_Origin;
    private Vector3 m_ActivePos;
    private bool m_Active = false;
    private bool m_Transitioning = false;
    [SerializeField]
    private float m_SlideSpeed = 1;
    private float m_SnapDistance = 0.0f;

    void Start()
    {
        m_MenuRect = GetComponent<RectTransform>();
        m_Origin = m_MenuRect.anchoredPosition;
        m_ActivePos = m_MenuRect.anchoredPosition;
        m_ActivePos.y += m_MenuRect.sizeDelta.y - (m_Arrow.GetComponent<RectTransform>().sizeDelta.y * 1.2f);
        m_SnapDistance = m_ActivePos.y * 0.05f;
    }

    public void MenuButton()
    {
        m_Active = !m_Active;
        m_Transitioning = true;
        if(m_Active)
        {
            m_Arrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            m_Arrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        }
    }

	void Update ()
    {
		if(m_Active)
        {
            if(m_Transitioning)
            {
                m_MenuRect.anchoredPosition = Vector3.Lerp(m_MenuRect.anchoredPosition, m_ActivePos, Time.deltaTime * m_SlideSpeed);
                if(Mathf.Abs(m_ActivePos.y - m_MenuRect.anchoredPosition.y) < m_SnapDistance)
                {
                    m_MenuRect.anchoredPosition = m_ActivePos;
                    m_Transitioning = false;
                }
            }
        }
        else
        {
            if (m_Transitioning)
            {
                m_MenuRect.anchoredPosition = Vector3.Lerp(m_MenuRect.anchoredPosition, m_Origin, Time.deltaTime * m_SlideSpeed);
                if (Mathf.Abs(m_Origin.y - m_MenuRect.anchoredPosition.y) < m_SnapDistance)
                {
                    m_MenuRect.anchoredPosition = m_Origin;
                    m_Transitioning = false;
                }
            }
        }
	}
}
