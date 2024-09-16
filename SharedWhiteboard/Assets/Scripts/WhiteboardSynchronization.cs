using System.Collections;
using Assets.Model;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class WhiteboardSynchronization : MonoBehaviour
    {
        [SerializeField]
        private SessionHandler _sessionHandler;

        [SerializeField]
        private WhiteboardDrawing _whiteboardDrawing;

        void Start()
        {
            StartCoroutine(nameof(Synchronize));
        }
        // Update is called once per frame
        void Update()
        {
        
        }

        private IEnumerator Synchronize()
        {
            yield return new WaitUntil(() => _sessionHandler.Initialized);

            while (true)
            {
                using (var request = UnityWebRequest.Get($"https://localhost:44342/api/Whiteboard?sessionId={_sessionHandler.SessionId}&participantId={_sessionHandler.ParticipantId}"))
                {
                    yield return request.SendWebRequest();

                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        yield break;
                    }

                    var whiteboardResult = request.downloadHandler.text;
                    var whiteboard = JsonConvert.DeserializeObject<Whiteboard>(whiteboardResult);

                    _whiteboardDrawing.UpdateWhiteboard(whiteboard);
                }

                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
