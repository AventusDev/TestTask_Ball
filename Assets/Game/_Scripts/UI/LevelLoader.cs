using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingUI;
        [SerializeField] private Image _loadingUIFillBar;
        public void ReloadLevel()
        {
            DOTween.KillAll();
            StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
        }
        private IEnumerator LoadSceneAsync(int sceneIndex)
        {
            DOTween.KillAll();
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            _loadingUI.SetActive(true);

            while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

                _loadingUIFillBar.fillAmount = progressValue;

                yield return 0;
            }
        }
    }
}
