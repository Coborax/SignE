using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SignE.Core.ECS;

namespace SignE.Core.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<Entity> WithComponent<T>(this IEnumerable<Entity> source) where T : IComponent
        {
            return source.Where(entity => entity.HasComponent<T>());
        }
    }
}