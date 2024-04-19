using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Publics
{
    [ControllerName("Public: Captcha")]
    [Route("Public/[controller]")]
    public class CaptchaController : BaseApiController
    {
        private readonly DataContext context;
        private readonly ILogger<AddressController> _logger;
        private readonly CaptchaService _captcha;

        public CaptchaController(DataContext _context, ILogger<AddressController> logger, CaptchaService captcha)
        {
            context = _context;
            _logger = logger;
            _captcha = captcha;
        }

        [HttpGet("{height}/{width}")]
        public async Task<IActionResult> Create(int height, int width)
        {
            var c = await _captcha.GenerateCaptcha(width,height);
            return Ok(c);
        }
    }
}

