using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace hexalambda
{
    public class Services
    {
        public async Task<string> GetConnection()
        {
            string parameterName = "db_postegres";

            using (var ssmClient = new AmazonSimpleSystemsManagementClient(RegionEndpoint.USEast1))
            {
                var request = new GetParameterRequest
                {
                    Name = parameterName,
                    WithDecryption = true
                };

                try
                {
                    var response = await ssmClient.GetParameterAsync(request);
                    return response.Parameter.Value;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao recuperar o valor do SecureString: {ex.Message}");
                }
            }

            return "";
        }
    }
}
