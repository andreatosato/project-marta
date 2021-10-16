using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMarta.Services
{
    public interface ISpeechService
    {
        Task SpeechAsync(string text);
    }
}
