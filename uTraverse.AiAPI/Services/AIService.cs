﻿using uTraverse.AiAPI.Exceptions;

namespace uTraverse.AiAPI.Services;

/// <summary>
/// Manages communication with the AI microservice
/// </summary>
/// <param name="logger">A logger instance for internal usage</param>
/// <param name="httpClient">An HttpClient instance for communication with the AI microservice (should have BaseAddress set to the AI microservice URL)</param>
public class AiService(ILogger<AiService> logger, HttpClient httpClient) : IAiService
{
    private readonly ILogger _logger = logger;

    public async Task<IEnumerable<Guid>> GetPlaceIdsAsync(string prompt)
    {
        // TODO: Add distributed caching to offload the AI and speed up execution

        _logger.LogDebug("Retrieving place IDs from the prompt: {prompt}", prompt);

        // Retrieve the IDs for the prompt (TODO: replace with a better endpoint URL)
        var res = await httpClient.GetFromJsonAsync<IEnumerable<Guid>>($"/?prompt={prompt}");

        // Throw an exception if received null
        if (res is null)
        {
            _logger.LogWarning("AI API returned a null response");
            throw new ApiResponseNullException("AI API returned a null response");
        }

        _logger.LogDebug("AI API request was successful");

        return res;
    }
}