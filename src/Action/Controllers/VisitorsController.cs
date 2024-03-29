﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Action.Controllers
{
	[Route("api/[controller]")]
	public class VisitorsController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;

		public VisitorsController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
		{
			_dbContext = dbContext;
			_htmlEncoder = htmlEncoder;
		}

		// GET: api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			try
			{
				if (_dbContext == null)
					return new[] { "No database connection" };
				var data = _dbContext.Visitors.Select(m => _htmlEncoder.Encode(m.Name)).ToList();
				return data;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		// POST api/values
		[HttpPost]
		public IEnumerable<string> Post([FromBody] Visitor visitor)
		{
			if (_dbContext == null)
			{
				return new[] { _htmlEncoder.Encode(visitor.Name) };
			}
			_dbContext.Visitors.Add(visitor);
			_dbContext.SaveChanges();
			return _dbContext.Visitors.Select(m => _htmlEncoder.Encode(m.Name)).ToList();
		}
	}
}