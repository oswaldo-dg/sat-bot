using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot_infrastructure
{
    public class FSSecretService : ISecretService
    {

        private readonly IConfiguration Configuration;
        private readonly string SecretsPath;
        public FSSecretService(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            SecretsPath = this.Configuration.GetSection("secrets:fs:path").Value;
        }

        private string SecretPath(string SecretId)
        {
            return Path.Combine(this.SecretsPath, $"{SecretId}.secret");
        }

        public async Task<string?> ReadString(string SecretId)
        {
            string file = SecretPath(SecretId);
            if (File.Exists(file))
            {
                var s = await File.ReadAllTextAsync(file);
                return s;
            }
            return null;
        }

        public async Task<bool> Write(string SecretId, string Value)
        {
            string file = SecretPath(SecretId);
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            await File.WriteAllTextAsync(file, Value);

            return true;
        }
 
    }
}
