using ECatalog.BLL.Services.Interfaces;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using Repository.Pattern.UnitOfWork;

namespace ECatalog.API.Providers
{
    public class RefreshTokenProvider: IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var container = (IUnityContainer)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUnityContainer));
            var auth = container.Resolve<IRefreshTokenFacade>();
            var refreshTokenLifeTime = "5000";

            //var userNameValue = context.Ticket.Identity.Name;
            var userNameValue = context.Ticket.Identity.Claims.FirstOrDefault(c => c.Type == "Name").Value;
            var token = auth.FindRefreshTokenNotExpired( userNameValue);
            if (token == null)
            {
                token = new RefreshTokenDto()
                {
                    Id = HashHelper.GetHash(Guid.NewGuid().ToString("n")),
                    UserName = userNameValue,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
                };
                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;
                token.ProtectedTicket = context.SerializeTicket();

                auth.AddRefreshToken(token);
                //AccessTokenValues userToken = new AccessTokenValues();
                //var TokenIsexist = Startup.activeTokens.TryGetValue(subjectValue, out userToken);

                //if (TokenIsexist)
                //{
                //    Startup.activeTokens[subjectValue].IsActive = true;
                //    Startup.activeTokens[subjectValue].IsPasswordChange = false;
                //}
                //else
                //{
                //    Startup.activeTokens.Add(subjectValue, new AccessTokenValues { IsActive = true, IsPasswordChange = false });
                //}
            }
            else
            {
                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;
                token.ProtectedTicket = context.SerializeTicket();
            }
            context.SetToken(token.Id);

        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            string hashedTokenId = context.Token;

            var container = (IUnityContainer)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUnityContainer));
            var refreshTokenFacade = container.Resolve<IRefreshTokenFacade>();
            var unit = container.Resolve<IUnitOfWorkAsync>();
            ////to get refresh token and not expired
            var refreshToken = refreshTokenFacade.FindRefreshTokenNotExpired(hashedTokenId);

            ////to get refresh token without validatation to the date
            //var refreshToken = await auth.FindRefreshToken(hashedTokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                //var result =  refreshTokenFacade.RemoveRefreshToken(hashedTokenId);
                //if (context.Ticket != null)
                //{
                //    context.SetTicket(context.Ticket);
                //}
            }
            else
            {
                //Add your flag to the header of the response
                context.Response.Headers.Add("X-Challenge",
                         new[] { ((int)HttpStatusCode.NotAcceptable).ToString() });
            }
        }
    }
}