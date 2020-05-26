﻿using Microsoft.Azure.Cosmos;
using SimulationWebservice.Models;
using System.Threading.Tasks;

namespace SimulationWebservice.Services
{
    /// <summary>
    /// Connect and use Azure Cosmos DB, including CRUD operations.
    /// </summary>

    public class CosmosDBService : ICosmosDbService
    {
        private Container container;

        /// <summary>
        /// Use client to connect place database connection in container.
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="dbName"></param>
        /// <param name="containerName"></param>
        public CosmosDBService(CosmosClient dbClient, string dbName, string containerName)
        {
            this.container = dbClient.GetContainer(dbName, containerName);
        }

        /// <summary>
        /// Add data item to container.
        /// </summary>
        /// <returns></returns>
        public async Task AddDataAsync(GensetData data)
        {
            await this.container.CreateItemAsync<GensetData>(data, new PartitionKey(data.Id));
        }

        /// <summary>
        /// Retreives data item from container.
        /// </summary>
        /// <returns></returns>
        private async Task<GensetData> GetDataAsync(string id)
        {
            try
            {
                ItemResponse<GensetData> response = await this.container.ReadItemAsync<GensetData>(id, new PartitionKey(id));

                return response.Resource;
            }
            catch
            {
                return null;
            }
        }
    }
}
