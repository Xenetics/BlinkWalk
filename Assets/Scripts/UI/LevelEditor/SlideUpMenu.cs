using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUpMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MenuBox;
    private Vector3 m_Origin;
    private Vector3 m_ActivePos;
    private bool m_Active = false;
    private bool m_Transitioning = false;

    void Start()
    {
        m_Origin = transform.position;
        m_ActivePos = m_MenuBox.GetComponent<RectTransform>().position;
        m_ActivePos.y += m_MenuBox.GetComponent<RectTransform>().sizeDelta.y - 20;
    }

    public void MenuButton()
    {
        m_Active = !m_Active;
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
