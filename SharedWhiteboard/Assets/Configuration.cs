using UnityEngine;

namespace Assets
{
    public class Configuration : MonoBehaviour
    {
        public static Configuration Instance { get; private set; }

        public Environment Environment;

        public string BackendUrl { get; private set; }

        private void Awake()
        {
            Instance = this;

            switch (Environment)
            {
                case Environment.Dev:
                    BackendUrl = "https://localhost:44342";
                    break;
                case Environment.Local:
                    BackendUrl = "http://192.168.1.71:5100";
                    break;
                case Environment.Prod:
                    BackendUrl = "TBD";
                    break;
            }
        }
    }
}
