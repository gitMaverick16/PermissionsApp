using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using PermissionsApp.Query.Application.Common.Interfaces;
using PermissionsApp.Query.Domain.Permissions;
using PermissionsApp.Query.Infrastructure.Configurations;

namespace PermissionsApp.Query.Infrastructure.Permissions.Persistence
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ElasticsearchClient _client;
        private readonly ElasticSettings _elasticSettings;
        public PermissionRepository(IOptions<ElasticSettings> optionsMonitor)
        {
            _elasticSettings = optionsMonitor.Value;

            var settings = new ElasticsearchClientSettings(new Uri(_elasticSettings.Url))
                .DefaultIndex(_elasticSettings.DefaultIndex);

            _client = new ElasticsearchClient(settings);
        }
        public async Task<bool> CreateOrModify(Permission permission)
        {
            var response = await _client.IndexAsync(permission, idx => idx.Index(_elasticSettings.DefaultIndex)
                .OpType(OpType.Index));
            return response.IsValidResponse;
        }

        public async Task<bool> CreateOrUpdateBulk(IEnumerable<Permission> permissions, string indexName)
        {
            var response = await _client.BulkAsync(idx => idx.Index(_elasticSettings.DefaultIndex)
                .UpdateMany(permissions,
                    (ud, u) => ud.Doc(u).DocAsUpsert(true)));
            return response.IsValidResponse;
        }

        public async Task CreateIndexIfNotExistsAsync(string indexName)
        {
            var existsResponse = await _client.Indices.ExistsAsync(indexName);
            if (!existsResponse.Exists)
            {
                await _client.Indices.CreateAsync(indexName);
            }
        }

        public async Task<Permission> GetByIdAsync(int id)
        {
            var response = await _client.GetAsync<Permission>(id,
                g => g.Index(_elasticSettings.DefaultIndex));
            return response.Source;
        }

        public async Task<List<Permission>> GetAllAsync()
        {
            var response = await _client.SearchAsync<Permission>(
                s => s.Index(_elasticSettings.DefaultIndex));
            return response.IsValidResponse ? response.Documents.ToList() : new List<Permission>();
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _client.DeleteAsync<Permission>(id,
                d => d.Index(_elasticSettings.DefaultIndex));
            return response.IsValidResponse;
        }
    }
}
