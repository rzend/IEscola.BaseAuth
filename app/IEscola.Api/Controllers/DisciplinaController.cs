using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using IEscola.Domain.Entities;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using IEscola.Api.Filters;
using IEscola.Core.Notificacoes.Interfaces;
using IEscola.Core.Controllers;
using IEscola.Core.Controllers.DefaultResponse;

namespace IEscola.Api.Controllers
{
    
    public class DisciplinaController : MainController
    {
        private readonly List<Disciplina> disciplinaList = new List<Disciplina> {
            new Disciplina ( Guid.Parse("292499B0-2B09-4787-92CF-8C352456EAE0"), "Matemática", "A melhor de todas"),
            new Disciplina ( Guid.Parse("506101B4-D597-44BE-921C-49069023E0DA"), "Língua Portuguesa", "A mais importante"),
            new Disciplina ( Guid.Parse("8C2A1F47-F991-4326-868A-09D38CBC0FA7"), "História", "Para entender de onde viemos"),
            new Disciplina ( Guid.Parse("FF829D06-A51E-413A-A800-8DA041F60AA6"), "Geografia","Para entender o mundo e para onde vamos"),
            new Disciplina ( Guid.Parse("67DA87F3-BD25-4ED2-B9EB-1420E98A9CDD"), "Física", "Para experimentar a Matemática"),
            new Disciplina ( Guid.Parse("F0267664-2D3C-4FCB-B94C-6DD3F5544E10"), "Química","Para aprender que NaCl é sal"),
            new Disciplina ( Guid.Parse("9AA2068E-4704-4E7A-A67F-44F426E195C1"), "Filosofia","Para viajar nas ideias")
        };


        public DisciplinaController(INotificador notificador) : base(notificador)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(SimpleResponseObject<IEnumerable<Disciplina>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [AuthorizationActionFilterAsync]
        public IActionResult Get()
        {
            return SimpleResponse(disciplinaList);
        }

        // GET api/<DisciplinaController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SimpleResponseObject<Disciplina>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SimpleResponseObject<Disciplina>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [AuthorizationActionFilterAsync]
        public IActionResult Get(Guid id)
        {
            var disciplina = disciplinaList.FirstOrDefault(d => d.Id == id);
            return SimpleResponse(disciplina);
        }

        // POST api/<DisciplinaController>
        [HttpPost]
        [ProducesResponseType(typeof(SimpleResponseObject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SimpleResponseObject), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Disciplina disciplina)
        {
            disciplinaList.Add(disciplina);
            return SimpleResponse();
        }

        // PUT api/<DisciplinaController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SimpleResponseObject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SimpleResponseObject), StatusCodes.Status400BadRequest)]
        public IActionResult Put(Guid id, [FromBody] Disciplina disciplina)
        {
            var disc = disciplinaList.FirstOrDefault(d => d.Id == id);

            if (disc != null)
            {
                disciplinaList.Remove(disc);
                disciplinaList.Add(disciplina);
            }
            return SimpleResponse();
        }

        // DELETE api/<DisciplinaController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SimpleResponseObject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SimpleResponseObject), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(Guid id)
        {
            var disc = disciplinaList.FirstOrDefault(d => d.Id == id);
            disciplinaList.Remove(disc);

            return SimpleResponse();
        }
    }
}
