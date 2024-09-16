using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class SessionHandler : MonoBehaviour
    {
        [CanBeNull] public string SessionId { get; private set; }
        [CanBeNull] public string ParticipantId { get; private set; }

        public bool Initialized => SessionId != null && ParticipantId != null;

        private void Start()
        {
            StartCoroutine(nameof(Initialize));
        }

        private IEnumerator Initialize()
        {
            using (var sessionRequest = UnityWebRequest.Get("https://localhost:44342/api/Session"))
            {
                sessionRequest.certificateHandler = new IgnoreCertificateHandler();
                yield return sessionRequest.SendWebRequest();

                if (sessionRequest.result != UnityWebRequest.Result.Success)
                {
                    yield break;
                }

                SessionId = sessionRequest.downloadHandler.text;
            }

            using var joinSessionRequest = UnityWebRequest.Get($"https://localhost:44342/api/Participant?sessionId={SessionId}");

            joinSessionRequest.certificateHandler = new IgnoreCertificateHandler();
            yield return joinSessionRequest.SendWebRequest();

            if (joinSessionRequest.result != UnityWebRequest.Result.Success)
            {
                yield break;
            }

            ParticipantId = joinSessionRequest.downloadHandler.text;
        }
    }
}
