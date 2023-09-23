using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace Acutipupu;

internal static class Pools
{
    public static ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider()
        .CreateStringBuilderPool();
}
