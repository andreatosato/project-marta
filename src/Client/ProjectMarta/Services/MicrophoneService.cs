using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ProjectMarta.Services
{
    class MicrophoneService : IMicrophoneService
    {
        public async Task<bool> GetPermissionAsync()
        {
            var microphonePermission = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (microphonePermission != PermissionStatus.Granted)
            {
                microphonePermission = await Permissions.RequestAsync<Permissions.Microphone>();
            }

            return microphonePermission == PermissionStatus.Granted;
        }

        public void OnRequestPermissionResult(bool isGranted)
        {
            throw new NotImplementedException();
        }
    }
}
