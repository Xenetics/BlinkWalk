using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUpMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Arrow;
    private Vector3 m_Origin;
    private Vector3 m_ActivePos;
    private bool m_Active = false;
    private bool m_Transitioning = false;

    void Start()
    {
        m_Origin = transform.position;
        m_ActivePos = GetComponent<RectTransform>().position;
        m_ActivePos.y += GetComponent<RectTransform>().sizeDelta.y - 20;
    }

    public void MenuButton()
    {
        m_Active = !m_Active;
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
            //lerp to active pos and snap when close
        }
        else
        {
            //lerp to origin and snap when close
        }
	}
}
