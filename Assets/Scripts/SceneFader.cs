using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

    public Image image;
    public AnimationCurve curve;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0, 0, 0, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0, 0, 0, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
