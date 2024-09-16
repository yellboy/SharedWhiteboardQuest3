using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class IgnoreCertificateHandler : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}