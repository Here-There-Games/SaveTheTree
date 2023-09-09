using UnityEngine.SceneManagement;

namespace Common.Utilities
{
    public static class Loader
    {
        public static void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}