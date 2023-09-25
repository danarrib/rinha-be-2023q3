using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinhaBackend2023Q3.Models;
using System.Globalization;

namespace RinhaBackend2023Q3.Controllers
{
    [ApiController]
    [Route("/")]
    public class PessoaController
    {
        protected readonly RinhaContext _context;

        public PessoaController(RinhaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("pessoas/{id}")]
        public ActionResult<Pessoa> GetPessoa(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                var pessoa = _context.Pessoas.Find(guid);

                return new OkObjectResult(pessoa);
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        [HttpGet]
        [Route("pessoas")]
        public ActionResult<List<Pessoa>> SearchPessoas(string t)
        {
            if (string.IsNullOrWhiteSpace(t))
                return new BadRequestResult();

            try
            {
                var pessoas = _context.Pessoas
                    .Where(x => x.SearchIndex.Contains(t.ToLower()))
                    .Take(50)
                    .ToList();

                return new OkObjectResult(pessoas);
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("pessoas")]
        public ActionResult AddPessoa(Pessoa pessoa)
        {
            // Validate entity
            if (!ValidatePessoa(pessoa))
                return new UnprocessableEntityResult();

            pessoa.Id = Guid.NewGuid();
            pessoa.SearchIndex = $"{pessoa.Nome.ToLower()} {pessoa.Apelido.ToLower()} {string.Join(" ", pessoa.Stack.Select(x => x.ToLower()))}";

            try
            {
                _context.Pessoas.Add(pessoa);
                _context.SaveChanges();

                return new CreatedResult($"/pessoas/{pessoa.Id}", null);
            }
            catch
            {
                return new UnprocessableEntityResult();
            }
        }

        [HttpGet]
        [Route("contagem-pessoas")]
        public ActionResult<List<Pessoa>> CountPessoas()
        {
            var qtdRegistros = _context.Pessoas.LongCount();
            return new OkObjectResult(qtdRegistros);
        }

        [HttpDelete]
        [Route("limpar-banco-de-dados")]
        public ActionResult LimparBanco()
        {
            _context.Database.ExecuteSqlRaw($"TRUNCATE TABLE [pessoas];");
            return new OkResult();
        }

        private bool ValidatePessoa(Pessoa pessoa)
        {
            if (pessoa == null
                || !ValidateStringLength(pessoa.Nome, 1, 100)
                || !ValidateStringLength(pessoa.Apelido, 1, 32)
                || !ValidateStringLength(pessoa.Nascimento, 10)
                )
                return false;

            if (pessoa.Stack != null)
                foreach (var item in pessoa.Stack)
                    if (!ValidateStringLength(item, 1, 32))
                        return false;

            if (!ValidateDate(pessoa.Nascimento))
                return false;

            return true;
        }

        private bool ValidateStringLength(string text, int minLength, int maxLength)
        {
            return text.Length >= minLength && text.Length <= maxLength;
        }
        private bool ValidateStringLength(string text, int exactLength)
        {
            return text.Length == exactLength;
        }

        private bool ValidateDate(string date)
        {
            return DateTime.TryParseExact(s: date, format: "yyyy-MM-dd", provider: null, style: DateTimeStyles.None, out _);
        }
    }
}
