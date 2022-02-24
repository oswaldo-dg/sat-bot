using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot_infrastructure
{
    public interface ISecretService
    {
        Task<string?> ReadString(string SecretId);
        Task<bool> Write(string SecretId, string Value);
    }
}
