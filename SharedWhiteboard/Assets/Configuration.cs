using UnityEngine;

namespace Assets
{
    public class Configuration : MonoBehaviour
    {
        public Environment Environment;

        public string BackendUrl { get; private set; }

        private void Awake()
        {
            switch (Environment)
            {
                case Environment.Local:
                    BackendUrl = "https://localhost:44342";
                    break;
                case Environment.Prod:
                    BackendUrl = "https:";
                    break;
            }
        }
    }
}
