﻿using Microsoft.EntityFrameworkCore;
using Portfolio.Domain;
using Portfolio.Domain.Monads;
using Portfolio.Queries.Interfaces;
using Portfolio.Queries.Seedwork;
using static Portfolio.Queries.ShareVolume.ShareVolumesViewModel;

namespace Portfolio.Queries.ShareVolume;

public sealed class GetSharesVolumeHandler : IQueryHandler<GetSharesVolume, ShareVolumesViewModel>
{
    private readonly IWalletQueryContext _walletQueryContext;

    public GetSharesVolumeHandler(IWalletQueryContext walletQueryContext)
    {
        _walletQueryContext = walletQueryContext;
    }

    public async Task<Result<ShareVolumesViewModel>> Handle(GetSharesVolume query, CancellationToken cancellation)
    {
        Wallet? wallet = await _walletQueryContext.Wallets.SingleOrDefaultAsync(w => w.Id == query.WalletId, cancellationToken: cancellation);

        if (wallet is null) return Result.Failure<ShareVolumesViewModel>("Wallet not found");

        var vm = new ShareVolumesViewModel(wallet.Shares.Select(shareVolume => new ShareVolumeViewModel(shareVolume.Symbol, shareVolume.Volume)));

        return Result.Success(vm);
    }
}