﻿using Portfolio.Domain.Monads;
using Portfolio.Proxies;
using Portfolio.Proxies.Dtos;
using Portfolio.Queries.Seedwork;

namespace Portfolio.Queries.Time;

public sealed class GetCurrentTimeHandler : IQueryHandler<GetCurrentTime, DateTime>
{
    private readonly ITimeProxy _client;

    public GetCurrentTimeHandler(ITimeProxy client)
    {
        _client = client;
    }

    public async Task<Result<DateTime>> Handle(GetCurrentTime query, CancellationToken cancellation)
    {
        CurrentTime? currentTime = await _client.GetCurrentTime();

        if (currentTime is null)
        {
            return Result.Failure<DateTime>("Failed to get current time");
        }
        
        return Result.Success(currentTime.Value);
    }
}