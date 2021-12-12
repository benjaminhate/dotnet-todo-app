using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Todo.Presentation.Extensions
{
    public static class EndpointsExtensions
    {
        public static IEndpointRouteBuilder Redirect(
            this IEndpointRouteBuilder endpoints,
            string from, string to)
        {
            return Redirect(endpoints,
                new Redirective(from, to));
        }
        
        public static IEndpointRouteBuilder RedirectPermanent(
            this IEndpointRouteBuilder endpoints,
            string from, string to)
        {
            return Redirect(endpoints,
                new Redirective(from, to, true));
        }

        public static IEndpointRouteBuilder Redirect(
            this IEndpointRouteBuilder endpoints,
            params Redirective[] paths
        )
        {
            foreach (var path in paths)
            {
                endpoints.MapGet(path.From, 
                    async http =>
                    {
                        http.Response.Redirect(
                            $"{path.To}{http.Request.QueryString}",
                            path.Permanent);
                    });
            }

            return endpoints;
        }
    }
    
    public struct Redirective
    {
        public Redirective(string from, string to, bool permanent = false)
        {
            From = from;
            To = to;
            Permanent = permanent;
        }
            
        public string From { get; set; }
        public string To { get; set; }
        public bool Permanent { get; set; }
    }
}