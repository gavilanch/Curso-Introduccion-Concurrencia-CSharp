using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms
{
    public static class TaskExtensionMethods
    {
        public static async Task<T> WithCancellation<T>(this Task<T> task,
            CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

            using (cancellationToken.Register(state =>
            {
                ((TaskCompletionSource<object>)state).TrySetResult(null);
            }, tcs))
            {
                var tareaResultante = await Task.WhenAny(task, tcs.Task);
                if (tareaResultante == tcs.Task)
                {
                    throw new OperationCanceledException(cancellationToken);
                }

                return await task;
            }

        }
    }
}
