using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Domain.Entities
{
    public class Locacao
    {
        public Guid Id { get; private set; }
        public Moto Moto { get; private set; }
        public Entregador Entregador { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataTerminoPrevista { get; private set; }
        public DateTime? DataTerminoReal { get; private set; }
        public decimal ValorTotal { get; private set; }
        public int PlanoId { get; private set; }
        public PlanoLocacao PlanoLocacao { get; private set; }

        // Construtor vazio para o EF Core
        private Locacao()
        {
        }

        // construtor privado para ser usado em regras de negócio
        private Locacao(Moto moto, Entregador entregador, DateTime dataInicio, DateTime dataTerminoPrevista, decimal valorTotal, PlanoLocacao planoLocacao)
        {
            Id = Guid.NewGuid();
            Moto = moto;
            Entregador = entregador;
            DataInicio = dataInicio;
            DataTerminoPrevista = dataTerminoPrevista;
            ValorTotal = valorTotal;
            PlanoId = planoLocacao.Id;
            PlanoLocacao = planoLocacao;
        }

        public static Locacao Create(Moto moto, Entregador entregador, DateTime dataInicio, DateTime dataTerminoPrevista, decimal valorTotal, PlanoLocacao planoLocacao)
        {
            return new Locacao(moto, entregador, dataInicio, dataTerminoPrevista, valorTotal, planoLocacao);
        }
        public void FinalizarLocacao(DateTime dataTerminoReal)
        {
            DataTerminoReal = dataTerminoReal;

            var diasPrevistos = (DataTerminoPrevista - DataInicio).Days;
            var diasUtilizados = (dataTerminoReal - DataInicio).Days;

            if (diasUtilizados < diasPrevistos)
            {
                // Devolução antecipada: aplicar multa
                var diasNaoUtilizados = diasPrevistos - diasUtilizados;
                var multa = PlanoLocacao.ValorDiaria * diasNaoUtilizados * (PlanoLocacao.Id == 7 ? 0.2m : 0.4m);
                ValorTotal = (PlanoLocacao.ValorDiaria * diasUtilizados) + multa;
            }
            else if (diasUtilizados > diasPrevistos)
            {
                // Devolução atrasada: cobrar diária extra
                var diasAdicionais = diasUtilizados - diasPrevistos;
                ValorTotal = (PlanoLocacao.ValorDiaria * diasPrevistos) + (50 * diasAdicionais);
            }
        }
    }
}
