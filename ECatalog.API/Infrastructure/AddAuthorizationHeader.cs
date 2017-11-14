using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace ECatalog.API.Infrastructure
{
    public class AddAuthorizationHeader : IOperationFilter
    {
        /// <summary>
        /// Adds an authorization header to the given operation in Swagger.
        /// </summary>
        /// <param name="operation">The Swashbuckle operation.</param>
        /// <param name="schemaRegistry">The Swashbuckle schema registry.</param>
        /// <param name="apiDescription">The Swashbuckle api description.</param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation == null) return;

            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            var parameter = new Parameter
            {
                description = "The authorization token",
                @in = "header",
                name = "Authorization",
                required = true,
                type = "string"
            };

            var parameter2 = new Parameter
            {
                description = "The language",
                @in = "header",
                name = "Accept-Language",
                required = true,
                type = "string",
                @default = "en"
            };
            
            if (apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                parameter.required = false;
            }

            operation.parameters.Add(parameter);
            operation.parameters.Add(parameter2);
        }
    }
}