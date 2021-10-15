using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMarta.Services
{
    public interface IMicrophoneService
    {
        Task<bool> GetPermissionAsync();
        void OnRequestPermissionResult(bool isGranted);
    }
}
