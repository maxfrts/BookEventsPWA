using Lib.Net.Http.WebPush;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace bookEventsPWA.Store
{
    public interface IPushSubscriptionStore
    {
        Task<int> StoreSubscriptionAsync(PushSubscription subscription);

        Task DiscardSubscriptionAsync(string endpoint);

        Task ForEachSubscriptionAsync(Action<PushSubscription> action);

        Task ForEachSubscriptionAsync(Action<PushSubscription> action, CancellationToken cancellationToken);

    }
}
