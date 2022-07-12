using ClientAddress.Infrastructure;
using ClientAddress.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAddress.Controllers
{
    public class AddressController : Controller
    {
        private readonly ClientAddressContext _context;

        public AddressController(ClientAddressContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: Client/Delete/5
        [HttpGet]
        public IActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var item = _context.Address.Where(c => c.ClientId == id).ToList();

            if (item is null)
            {
                return NotFound();
            }

            IEnumerable<AddressElement> viewModel = item.Select(c => new AddressElement()
            {
                Id = c.Id,
                Description = c.Description,
            });

            AddressViewModel view = new AddressViewModel();
            view.ClientId = id.Value;
            view.Elements = viewModel;

            return View(view);
        }

        //// GET: Client/Create
        public ActionResult Create(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var createModel = new AddressForCreateEditModel();
            createModel.ClientId = id.Value;

            return View(createModel);
        }

        //// POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddressForCreateEditModel createViewModel)
        {
            var newAddress = new Address();

            newAddress.Description = createViewModel.Description;
            newAddress.ClientId = createViewModel.ClientId;

            _context.Address.Add(newAddress);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), "Address", new { id = createViewModel.ClientId });
        }

        //// GET: Client/Edit/5
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var item = _context.Address.FirstOrDefault(c => c.Id == id);

            if (item is null)
            {
                return NotFound();
            }

            var addressEdit = new AddressForCreateEditModel();
            addressEdit.Description = item.Description;
            addressEdit.ClientId = item.ClientId;

            return View(addressEdit);
        }

        //// POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AddressElement element)
        {
            try
            {
                var foundAddress = _context.Address.FirstOrDefault(c => c.Id == element.Id);

                foundAddress.Description = element.Description;
                foundAddress.ClientId = element.ClientId;

                _context.Address.Update(foundAddress);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index), "Address", new { id = element.ClientId });
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

            var item = _context.Address.FirstOrDefault(c => c.Id == id);

            if (item is null)
            {
                return NotFound();
            }

            var addressDelete = new AddressElement();
            addressDelete.Description = item.Description;
            addressDelete.ClientId = item.ClientId;
            addressDelete.Id = item.Id;

            return View(addressDelete);
        }

        // POST: Client/Delete/5
        [HttpPost]
        public IActionResult Delete(AddressElement model)
        {
            if (model.Id == 0)
            {
                return BadRequest();
            }

            try
            {
                var foundAddress = _context.Address.FirstOrDefault(c => c.Id == model.Id);

                int clientId = foundAddress.ClientId;

                _context.Address.Remove(foundAddress);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index), "Address", new { id = clientId });
            }
            catch
            {
                return View();
            }
        }
    }
}