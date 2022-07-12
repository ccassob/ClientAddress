using ClientAddress.Infrastructure;
using ClientAddress.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAddress.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientAddressContext _context;

        public ClientController(ClientAddressContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            var model = await _context.Clients
                .Include(c => c.Address)
                .ToListAsync();

            IEnumerable<ClientElement> viewModel = model.Select(c => new ClientElement()
            {
                Id = c.Id,
                Name = c.Name,
                LastName = c.LastName,
                AddressCount = c.Address.Count
            });

            return View(viewModel);
        }

        //// GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClientForCreateEditModel createViewModel)
        {
            var client = new Client();

            client.Name = createViewModel.Name;
            client.LastName = createViewModel.LastName;

            _context.Clients.Add(client);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //// GET: Client/Edit/5
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var item = _context.Clients.FirstOrDefault(c => c.Id == id);

            if (item is null)
            {
                return NotFound();
            }

            var clientViewModel = new ClientForCreateEditModel();
            clientViewModel.Name = item.Name;
            clientViewModel.LastName = item.LastName;

            return View(clientViewModel);
        }

        //// POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClientElement editViewModel)
        {
            try
            {
                var foundClient = _context.Clients.FirstOrDefault(c => c.Id == editViewModel.Id);

                foundClient.LastName = editViewModel.LastName;
                foundClient.Name = editViewModel.Name;

                _context.Clients.Update(foundClient);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var item = _context.Clients.FirstOrDefault(c => c.Id == id);

            if (item is null)
            {
                return NotFound();
            }

            var clientViewModel = new ClientElement();
            clientViewModel.Id = item.Id;
            clientViewModel.Name = item.Name;
            clientViewModel.LastName = item.LastName;

            return View(clientViewModel);
        }

        // POST: Client/Delete/5
        [HttpPost]
        public IActionResult Delete(ClientElement model)
        {
            if (model.Id == 0)
            {
                return BadRequest();
            }

            try
            {
                var foundClient = _context.Clients.FirstOrDefault(c => c.Id == model.Id);

                _context.Clients.Remove(foundClient);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}