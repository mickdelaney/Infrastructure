using System;
using System.Web;
using Md.Infrastructure.Data;
using NHibernate;

namespace Md.Web.Mvc.Transactions
{
    public class UnitOfWorkModule : IHttpModule
    {
        private readonly ISessionFactory _factory;
        private UnitOfWork _unitOfWork;

        public UnitOfWorkModule(ISessionFactory factory)
        {
            _factory = factory;
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeforeRequest;
            context.EndRequest += AfterRequest;
        }
        public void Dispose()
        {
        }

        protected void BeforeRequest(object sender, EventArgs e)
        {
            _unitOfWork = new UnitOfWork(_factory);
            _unitOfWork.Start();
        }
        protected void AfterRequest(object sender, EventArgs e)
        {
            _unitOfWork.End();
        }
    }
}