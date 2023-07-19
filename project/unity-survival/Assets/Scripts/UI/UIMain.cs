using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMain : UIBase
{
	public Button m_PlayBtn;
	public void Start()
    {
		m_PlayBtn.onClick.AddListener(OnClickPlay);
    }

    private void OnClickPlay()
    {
        SceneManager.LoadScene("Play");
    }
}
