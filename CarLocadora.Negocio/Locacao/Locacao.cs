using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Models;
using CarLocadora.Modelo.Models.SeguroModel;
using CarLocadora.Negocio.Rabbit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Locacao
{
    public class Locacao : Ilocacao
    {
        #region Chamando Interface

        private readonly EntityContext _entityContext;
        private readonly IMensageria _mensageria;
        public Locacao(EntityContext entityContext, IMensageria mensageria)
        {
            _entityContext = entityContext;
            _mensageria = mensageria;
        }

        #endregion

        public async Task AlterarLocacao(LocacoesModel locacoesModel)
        {
            locacoesModel.DataAlteracao = DateTime.Now;
            _entityContext.Locacoes.Update(locacoesModel);
            await _entityContext.SaveChangesAsync();
        }


        public async Task IncluirLocacao(LocacoesModel locacoesModel)
        {
            locacoesModel.DataInclusao = DateTime.Now;
            await _entityContext.Locacoes.AddAsync(locacoesModel);
            await _entityContext.SaveChangesAsync();

            var cliente = _entityContext.Clientes.SingleOrDefault(x => x.CPF == locacoesModel.ClienteCPF);
            var veiculo = _entityContext.Veiculos.SingleOrDefault(x => x.Placa == locacoesModel.VeiculoPlaca);

            var seguroModel = new SeguroModel()
            {
                locacaoId = locacoesModel.Id,
                cpf = locacoesModel.ClienteCPF,
                nome = cliente.Nome,
                placa = locacoesModel.VeiculoPlaca,
                cnh = cliente.CNH,
                dataNascimento = cliente.DataNascimento,
                telefone = cliente.Telefone,
                marca = veiculo.Marca,
                modelo = veiculo.Modelo,
                combustivel = veiculo.Combustivel,
                dataHoraRetiradaPrevista = locacoesModel.DataHoraRetiradaPrevista,
                dataHoraDevolucaoPrevista = locacoesModel.DataHoraDevolucaoPrevista
            };

            _mensageria.EnviarMensagemRabbit(seguroModel, "", "seguro");
        }

        public async Task<List<LocacoesModel>> ListaLocacoes()
        {
            return await _entityContext.Locacoes.ToListAsync();
        }

        public async Task<LocacoesModel> ObterUmaLocacoes(int valor)
        {
            return await _entityContext.Locacoes.SingleAsync(x => x.Id.Equals(valor));
        }
    }
}
