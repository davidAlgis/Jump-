using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;

    [SerializeField]
    private Text m_messageText = default;
    [SerializeField]
    private Text m_messageDebug = default;

    public static UIManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.Find("UIManager").GetComponent<UIManager>();
            }
            return m_instance;
        }
    }

    public Text MessageText { get => m_messageText; set => m_messageText = value; }

    public void enableDisableMessageOnScreen(string message, bool enable)
    {
        m_messageText.enabled = enable;
        m_messageText.text = message;
    }

    public void debugMessageOnScreen(string message)
    {
        m_messageDebug.text = message;
    }

    public void fadeOut(Text text, float fadeOutTime = 2.0f)
    {
        StartCoroutine(FadeOutRoutine(text, fadeOutTime));
    }

    private IEnumerator FadeOutRoutine(Text text, float fadeOutTime)
    {
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }

}
