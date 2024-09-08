using Events.Domain.DTO.LiveEventDtos;
using Events.Domain.Entities;
using System.Linq.Expressions;
using System.Security.Cryptography;

using System.Text;

namespace Events.Business.Utility
{
    public static class ServiceExtensions
    {
        public static string GetHash(this string plainText)
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(plainText));
            return Convert.ToBase64String(hash);
        }

        public static DateOnly ToDateOnly(this DateTime time)
        {
            return new DateOnly(time.Year, time.Month, time.Day);
        }

        public static Expression<Func<LiveEvent, bool>> ToPredicate(this CriteriaDto dto)
        {
            Expression<Func<LiveEvent, bool>> filter = e => true;

            if (dto.Date.HasValue)
            {
                var date = dto.Date.Value;
                filter = CombineExpressions(filter, e => e.Date == date);
            }

            if (!string.IsNullOrEmpty(dto.Place))
            {
                var place = dto.Place;
                filter = CombineExpressions(filter, e => e.Place == place);
            }

            if (!string.IsNullOrEmpty(dto.Category))
            {
                var category = dto.Category;
                filter = CombineExpressions(filter, e => e.Category == category);
            }

            return filter;
        }

        private static Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
