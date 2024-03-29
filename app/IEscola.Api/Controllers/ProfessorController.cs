﻿using IEscola.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using IEscola.Core.Controllers;
using IEscola.Core.Notificacoes.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IEscola.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProfessorController : MainController
    {
        private readonly IProfessorService _service;

        public ProfessorController(INotificador notificador) : base(notificador)
        {

        }

        // GET: api/<ProfessorController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(professorList);
        }

        // GET api/<ProfessorController>/5
        [HttpGet("{id}")]
        // [ProducesResponseType(Type = typeof(IEnumerable<Professor>), StatusCode = 200)] // TODO: Verificar o motivo do erro
        public IActionResult Get(Guid id)
        {
            if  ( Guid.Empty == id)
                return BadRequest("id inválido");

            var professor = professorList.FirstOrDefault(p => p.Id == id);
            if (professor != null)
                professor.Tratamento = Constantes.TratamentoProfessor;

            return Ok(professor);
        }

        // POST api/<ProfessorController>
        [HttpPost]
        public IActionResult Post([FromBody] Professor professor)
        {


            return Ok();
        }

        // PUT api/<ProfessorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Professor professor)
        {
            return Ok();
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }
    }
}
