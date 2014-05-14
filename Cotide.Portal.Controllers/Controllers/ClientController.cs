using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cotide.Domain;
using Cotide.Domain.Contracts.Commands.Client;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Contracts.Repositories.Base;
using Cotide.Domain.Contracts.Tasks;
using Cotide.Domain.Dtos;
using Cotide.Domain.Entity;
using Cotide.Portal.Controllers.Controllers.Base;
using Cotide.Domain.Enum;
using Cotide.Framework.Extensions;

namespace Cotide.Portal.Controllers.Controllers
{

    /// <summary>
    /// 客户端管理
    /// </summary>
    public class ClientController : BaseController
    {
        protected IClientQueryService ClientQueryService;

        protected IClientTask ClientTask;

        public ClientController(
            IClientQueryService clientQueryService,
            IClientTask clientTask)
        {
            ClientQueryService = clientQueryService;
            ClientTask = clientTask;
        }

        [HttpGet]
        public ActionResult List(Guid? id)
        {
            var list = ClientQueryService.FindAll();
            if (id != null)
            {
                var client = ClientQueryService.Get((Guid)id);
                if (client == null)
                    return Redirect("List");


                ViewData["client"] = client;
                ViewData["Type"] = EnumExtensions.GetSelectList<ClientState>(client.ClientState);
                return View("List",list);
            }
              
            ViewData["Type"] = EnumExtensions.GetSelectList<ClientState>(); 
            return View("List",list);
        }


        public ActionResult Add(ClientDto client)
        {
            ClientTask.Create(new CreateClientCommand(
                client.Name,
                client.UserName,
                client.Paw,
                client.ClientSecret,
                client.ClientIdentifier)
            {
                RedirectUrl = client.RedirectUrl,
                ClientState = client.ClientState,
                Desc = client.Desc,
                Img = client.Img
            });
            return Redirect(Url.Action("List"));
        }


        public ActionResult Update(ClientDto viewModel)
        {
            var client = ClientQueryService.Get(viewModel.Id);
            if (client == null)
                return List(viewModel.Id);

            ClientTask.Update(new UpdateClientCommand(viewModel.Id)
            {
                RedirectUrl = viewModel.RedirectUrl,
                ClientIdentifier = viewModel.ClientIdentifier,
                ClientSecret = viewModel.ClientSecret,
                Paw = viewModel.Paw,
                UserName = viewModel.UserName,
                Name = viewModel.Name,
                ClientState = viewModel.ClientState,
                Desc = viewModel.Desc,
                Img = viewModel.Img
            });
            return Redirect(Url.Action("List", new { @id = client.Id }));
        }



        public ActionResult Delete(Guid clientId)
        {
            ClientTask.Delete(new DeleteClientCommand(clientId));
            return Redirect("List");
        }

    }
}